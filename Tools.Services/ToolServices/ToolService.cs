using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;
using Tools.Services.ToolGroupServices;
using Tools.Services.ToolServices.Enums;
using Tools.Services.ToolServices.Helpers;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Services.ToolServices
{
    public class ToolService : IToolService
    {
        private readonly IGenericRepository<ToolEntity> _toolRepository;
        private readonly IToolGroupService _toolGroupService;
        private readonly IToolSubgroupService _toolSubgroupService;

        public ToolService(IGenericRepository<ToolEntity> toolRepository,
            IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService)
        {
            _toolRepository = toolRepository;
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;
        }

        public async Task<ResponseService<long>> Create(ToolEntity toolEntity)
        {
            try
            {
                await _toolRepository.Create(toolEntity);
                return ResponseService<long>.Ok(toolEntity.Id);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error($"ToolService -> Create exception: {ex.Message}");
            }
        }

        public async Task<ToolsSortingGetModel> Sorting(ToolsSortingPostModel vm)
        {
            IQueryable<ToolEntity> query = _toolRepository.GetAll()
                .Include(tool => tool.Subgroup)
                .Include(tool => tool.Subgroup.Group);

            query = FilterQueryByRegistration(query, vm.Registration);
            query = FilterQueryByOrganizationUnitType(query, vm.OrganizationalUnit);
            query = FilterQueryByGroup(query, vm.GroupName);
            query = FilterQueryBySubgroup(query, vm.SubgroupName);
            query = FilterQueryByExpiration(query, vm.ExpirationCriteria);

            List<ToolEntity> tools = query.ToList();
            List<ToolsPostModel> posts = new List<ToolsPostModel>();
            foreach (ToolEntity tool in tools)
            {
                posts.Add(new ToolsPostModel()
                {
                    Brand = tool.Brand,
                    Group = tool.Subgroup.Group.Name,
                    Name = tool.Name,
                    OranizationUnit = tool.OrganizationalType.ToString(),
                    Subgroup = tool.Subgroup.Name,
                });
            }

            ToolsSortingGetModel getVm = new ToolsSortingGetModel()
            {
                ExpirationCriteria = vm.ExpirationCriteria,
                GroupName = vm.GroupName,
                OrganizationalUnit = vm.OrganizationalUnit,
                Registration = vm.Registration,
                SubgroupName = vm.SubgroupName,
                Tools = posts,
            };

            return getVm;
        }

        public async Task<ResponseService<ToolEntity>> ValidateBeforeCreating(CreateToolEntityPostModel vm)
        {
            OrganizationalUnitType organizationalUnit = OrganizationalUnitDisplay.GetEnumFromDisplay(vm.OrganizationUnit);
            if (organizationalUnit == 0) return ResponseService<ToolEntity>.Error(Errors.INVALID_VALUE_ERROR);

            if (!DateTime.TryParse(vm.CommissioningDate, out DateTime commisioningDate)) return ResponseService<ToolEntity>.Error(Errors.INVALID_DATE);
            if (!DateTime.TryParse(vm.CreatingDate, out DateTime creatingDate)) return ResponseService<ToolEntity>.Error(Errors.INVALID_DATE);

            RegistrationType registrationType = RegistrationTypeHelper.GetEnumAsStringFromDisplayName(vm.Registration);
            if (registrationType == 0) return ResponseService<ToolEntity>.Error(Errors.INVALID_VALUE_ERROR);

            var response = await _toolSubgroupService.GetByName(vm.Subgroup);
            if (response.IsError) return ResponseService<ToolEntity>.Error(response.ErrorMessage);
            long subgroupId = response.Value.Id;

            ToolEntity dbRecord = new ToolEntity()
            {
                Brand = vm.Brand,
                CommissioningDate = commisioningDate,
                CreatingDate = creatingDate,
                ExpirationYear = vm.ExpirationYear,
                FactoryNumber = vm.FactoryNumber,
                IntraFactoryNumber = vm.IntraFactoryNumber,
                Name = vm.Name,
                OrganizationalType = organizationalUnit,
                Registration = registrationType,
                RegistrationNumber = vm.RegistrationNumber,
                SubgroupFK = subgroupId,
            };

            return ResponseService<ToolEntity>.Ok(dbRecord);
        }

        private IQueryable<ToolEntity> FilterQueryByRegistration(IQueryable<ToolEntity> query, RegistrationType registration)
        {
            if (registration == 0)
            {
                return query;
            }
            return query.Where(tool => tool.Registration == registration);
        }

        private IQueryable<ToolEntity> FilterQueryByOrganizationUnitType(IQueryable<ToolEntity> query, OrganizationalUnitType organizationalUnit)
        {
            if (organizationalUnit == 0)
            {
                return query;
            }
            return query.Where(tool => tool.OrganizationalType == organizationalUnit);
        }

        private IQueryable<ToolEntity> FilterQueryByGroup(IQueryable<ToolEntity> query, string groupName)
        {
            if (groupName == null)
            {
                return query;
            }
            return query.Where(tool => tool.Subgroup.Group.Name == groupName);
        }

        private IQueryable<ToolEntity> FilterQueryBySubgroup(IQueryable<ToolEntity> query, string subgroupName)
        {
            if (subgroupName == null)
            {
                return query;
            }
            return query.Where(tool => tool.Subgroup.Name == subgroupName);
        }

        private IQueryable<ToolEntity> FilterQueryByExpiration(IQueryable<ToolEntity> query, ExpirationSortingCriteria expirationCriteria)
        {
            switch (expirationCriteria)
            {
                case ExpirationSortingCriteria.NonExpired:
                    {
                        return query.Where(tool => tool.ExpirationYear > DateTime.Now.Year);
                    }
                case ExpirationSortingCriteria.Expired:
                    {
                        return query.Where(tool => tool.ExpirationYear <= DateTime.Now.Year);
                    }
                default:
                    {
                        return query;
                    }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.OrganizationUnitServices;
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
        private readonly IOrganizationUnitService _organizationUnitService;

        public ToolService(IGenericRepository<ToolEntity> toolRepository,
            IToolGroupService toolGroupService,
            IToolSubgroupService toolSubgroupService,
            IOrganizationUnitService organizationUnitService)
        {
            _toolRepository = toolRepository;
            _toolGroupService = toolGroupService;
            _toolSubgroupService = toolSubgroupService;
            _organizationUnitService = organizationUnitService;
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
            IQueryable<ToolEntity> query = _toolRepository.Table
                .Include(tool => tool.OrganizationUnit)
                .Include(tool => tool.Subgroup)
                .Include(tool => tool.Subgroup.Group);

            query = FilterQueryByRegistration(query, vm.Registration);
            query = FilterQueryByOrganizationUnitType(query, vm.OrganizationalUnitName);
            query = FilterQueryByGroup(query, vm.GroupName);
            query = FilterQueryBySubgroup(query, vm.SubgroupName);
            query = FilterQueryByExpiration(query, vm.ExpirationCriteria);

            List<ToolEntity> tools = await query.ToListAsync();
            List<ToolsPostModel> posts = new List<ToolsPostModel>();
            foreach (ToolEntity tool in tools)
            {
                posts.Add(new ToolsPostModel()
                {
                    Id = tool.Id,
                    Brand = tool.Brand,
                    Group = tool.Subgroup.Group.Name,
                    Name = tool.Name,
                    OranizationUnit = tool.OrganizationUnit.Name,
                    Subgroup = tool.Subgroup.Name,
                });
            }

            ToolsSortingGetModel getVm = new ToolsSortingGetModel()
            {
                /*ExpirationCriteria = vm.ExpirationCriteria,
                GroupName = vm.GroupName,
                OrganizationalUnit = vm.OrganizationalUnit,
                Registration = vm.Registration,
                SubgroupName = vm.SubgroupName,*/
                Tools = posts,
            };

            return getVm;
        }

        public async Task<ResponseService<ToolEntity>> ValidateBeforeCreating(CreateToolEntityPostModel vm)
        {
            if (!DateTime.TryParse(vm.CommissioningDate, out DateTime commisioningDate)) return ResponseService<ToolEntity>.Error(Errors.INVALID_DATE);
            if (!DateTime.TryParse(vm.CreatingDate, out DateTime creatingDate)) return ResponseService<ToolEntity>.Error(Errors.INVALID_DATE);

            RegistrationType registrationType = RegistrationTypeHelper.GetEnumAsStringFromDisplayName(vm.Registration);
            if (registrationType == 0) registrationType = RegistrationType.NonRegister;

            var response = await _toolSubgroupService.GetByName(vm.Subgroup);
            if (response.IsError) return ResponseService<ToolEntity>.Error(response.ErrorMessage);
            long subgroupId = response.Value.Id;

            var unitResponse = await _organizationUnitService.GetByName(vm.OrganizationUnit);
            if (unitResponse.IsError) return ResponseService<ToolEntity>.Error(unitResponse.ErrorMessage);

            ToolEntity dbRecord = new ToolEntity()
            {
                Brand = vm.Brand,
                CommissioningDate = commisioningDate,
                CreatingDate = creatingDate,
                ExpirationYear = vm.ExpirationYear,
                FactoryNumber = vm.FactoryNumber,
                IntraFactoryNumber = vm.IntraFactoryNumber,
                Name = vm.Name,
                Manufacturer = vm.Manufacturer,
                OrganizationUnitFK = unitResponse.Value.Id,
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

        private IQueryable<ToolEntity> FilterQueryByOrganizationUnitType(IQueryable<ToolEntity> query, string unitName)
        {
            if (string.IsNullOrEmpty(unitName))
            {
                return query;
            }
            return query.Where(tool => tool.OrganizationUnit.Name == unitName);
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
                        return query.Where(tool => tool.CommissioningDate.AddYears(tool.ExpirationYear) > DateTime.Now);
                    }
                case ExpirationSortingCriteria.Expired:
                    {
                        return query.Where(tool => tool.CommissioningDate.AddYears(tool.ExpirationYear) <= DateTime.Now);
                    }
                default:
                    {
                        return query;
                    }
            }
        }

        public async Task<ResponseService<ToolEntity>> GetById(long id)
        {
            ToolEntity dbRecord = await _toolRepository.GetAll(tool => tool.Id == id)
                .Include(tool => tool.Examinutions)
                .Include(tool => tool.OrganizationUnit)
                .FirstOrDefaultAsync();

            if (dbRecord == null)
            {
                return ResponseService<ToolEntity>.Error(Errors.NOT_FOUND_ERROR);
            }
            return ResponseService<ToolEntity>.Ok(dbRecord);
        }

        public async Task<ICollection<ToolEntity>> GetById(ICollection<long> ids)
        {
            ICollection<ToolEntity> dbRecords = new List<ToolEntity>(ids.Count);
            foreach (long id in ids)
            {
                var dbRecord = await _toolRepository.GetById(id);
                if (dbRecord != null)
                {
                    dbRecords.Add(dbRecord);
                }
            }
            return dbRecords;
        }

        public async Task<ResponseService<long>> Delete(ToolEntity toolEntity)
        {
            try
            {
                await _toolRepository.Delete(toolEntity);
                return ResponseService<long>.Ok(toolEntity.Id);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }
        }

        public async Task<ResponseService> Update(ToolEntity toolEntity)
        {
            try
            {
                await _toolRepository.Update(toolEntity);
                return ResponseService.Ok();
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
        }

        public async Task<ResponseService<ToolEntity>> ValidateBeforeUpdating(UpdateToolPostModel vm)
        {
            ToolEntity dbRecord = await _toolRepository.GetById(vm.Id);
            if (dbRecord == null)
            {
                return ResponseService<ToolEntity>.Error(Errors.NOT_FOUND_ERROR);
            }

            if (!DateTime.TryParse(vm.CommissioningDate, out DateTime commisioningDate)) return ResponseService<ToolEntity>.Error(Errors.INVALID_DATE);
            if (!DateTime.TryParse(vm.CreatingDate, out DateTime creatingDate)) return ResponseService<ToolEntity>.Error(Errors.INVALID_DATE);

            RegistrationType registrationType = RegistrationTypeHelper.GetEnumAsStringFromDisplayName(vm.Registration);
            if (registrationType == 0) registrationType = RegistrationType.NonRegister;

            var response = await _toolSubgroupService.GetByName(vm.Subgroup);
            if (response.IsError) return ResponseService<ToolEntity>.Error(response.ErrorMessage);
            long subgroupId = response.Value.Id;

            var unitResponse = await _organizationUnitService.GetByName(vm.OrganizationUnit);
            if (unitResponse.IsError) return ResponseService<ToolEntity>.Error(unitResponse.ErrorMessage);

            dbRecord.Brand = vm.Brand;
            dbRecord.CommissioningDate = commisioningDate;
            dbRecord.CreatingDate = creatingDate;
            dbRecord.ExpirationYear = vm.ExpirationYear;
            dbRecord.FactoryNumber = vm.FactoryNumber;
            dbRecord.IntraFactoryNumber = vm.IntraFactoryNumber;
            dbRecord.Name = vm.Name;
            dbRecord.Manufacturer = vm.Manufacturer;
            dbRecord.OrganizationUnitFK = unitResponse.Value.Id;
            dbRecord.Registration = registrationType;
            dbRecord.RegistrationNumber = vm.RegistrationNumber;
            dbRecord.SubgroupFK = subgroupId;

            return ResponseService<ToolEntity>.Ok(dbRecord);
        }
    }
}
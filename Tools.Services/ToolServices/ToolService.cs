using System;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.Database.Enums;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.Response;
using Tools.Services.ToolServices.Helpers;
using Tools.Services.ToolServices.Models;
using Tools.Services.ToolSubgroupServices;

namespace Tools.Services.ToolServices
{
    public class ToolService : IToolService
    {
        private readonly IGenericRepository<ToolEntity> _toolRepository;
        private readonly IToolSubgroupService _toolSubgroupService;

        public ToolService(IGenericRepository<ToolEntity> toolRepository,
            IToolSubgroupService toolSubgroupService)
        {
            _toolRepository = toolRepository;
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

        public async Task<ResponseService<ToolEntity>> ValidateBeforeCreating(CreateToolEntityPostModel vm)
        {
            OrganizationalUnitType organizationalUnit = OrganizationUnitHelper.GetEnumAsStringFromDisplayName(vm.OrganizationUnit);
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
    }
}
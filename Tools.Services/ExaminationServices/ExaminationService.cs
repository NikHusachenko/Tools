﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Database.Entities;
using Tools.EntityFramework.GenericRepository;
using Tools.Services.ExaminationNatureServices;
using Tools.Services.ExaminationReasonServices;
using Tools.Services.ExaminationServices.Models;
using Tools.Services.ExaminationTypeService;
using Tools.Services.Response;

namespace Tools.Services.ExaminationServices
{
    public class ExaminationService : IExaminationService
    {
        private readonly IGenericRepository<ExaminationEntity> _examinationRepository;
        private readonly IExaminationNatureService _examinationNatureService;
        private readonly IExaminationReasonService _examinationReasonService;
        private readonly IExaminationTypeService _examinationTypeService;

        public ExaminationService(IGenericRepository<ExaminationEntity> examinationRepository,
            IExaminationNatureService examinationNatureService,
            IExaminationTypeService examinationTypeService,
            IExaminationReasonService examinationReasonService)
        {
            _examinationRepository = examinationRepository;
            _examinationNatureService = examinationNatureService;
            _examinationReasonService = examinationReasonService;
            _examinationTypeService = examinationTypeService;
        }

        public async Task<ResponseService<long>> Create(CreateExaminationPostModel vm)
        {
            var natureResponse = await _examinationNatureService.GetByName(vm.ExaminationNatureName);
            if (natureResponse.IsError)
            {
                return ResponseService<long>.Error(natureResponse.ErrorMessage);
            }

            var reasonResponse = await _examinationReasonService.GetByName(vm.ExaminationReasonName);
            if (reasonResponse.IsError)
            {
                return ResponseService<long>.Error(reasonResponse.ErrorMessage);
            }

            var typeResponse = await _examinationTypeService.GetByName(vm.ExaminationTypeName);
            if (typeResponse.IsError)
            {
                return ResponseService<long>.Error(typeResponse.ErrorMessage);
            }

            ExaminationEntity dbrecord = new ExaminationEntity()
            {
                ActualExaminationDate = vm.FactDate,
                ExaminationNatureFK = natureResponse.Value.Id,
                ExaminationReasonFK = reasonResponse.Value.Id,
                ExaminationResult = vm.ExaminationResult,
                ExaminationTypeFK = typeResponse.Value.Id,
                ScheduleExaminationDate = vm.ScheduleDate,
                ToolFK = vm.ToolFK,
            };

            try
            {
                await _examinationRepository.Create(dbrecord);
            }
            catch (Exception ex)
            {
                return ResponseService<long>.Error(ex.Message);
            }
            return ResponseService<long>.Ok(dbrecord.Id);
        }

        public async Task<ResponseService> Delete(long id)
        {
            ExaminationEntity dbrecord = await _examinationRepository.GetById(id);
            if (dbrecord == null)
            {
                return ResponseService.Error(Errors.NOT_FOUND_ERROR);
            }

            try
            {
                await _examinationRepository.Delete(dbrecord);
            }
            catch (Exception ex)
            {
                return ResponseService.Error(ex.Message);
            }
            return ResponseService.Ok();
        }

        public async Task<ICollection<ExaminationEntity>> GetByToolFK(long toolFk)
        {
            return await _examinationRepository.GetAll()
                .Where(examination => examination.ToolFK == toolFk)
                .Include(examination => examination.ExaminationNature)
                .Include(examination => examination.ExaminationReason)
                .Include(examination => examination.ExaminationType)
                .ToListAsync();
        }
    }
}
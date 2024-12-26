using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IAzureStorageService _storageService;
        private readonly string _containerName;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration,
            IAzureStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
            _storageService = storageService;
        }

        public async Task<List<TeacherViewModel>> GetTeacherList()
        {
            return _mapper.Map<List<TeacherViewModel>>(await _unitOfWork.TeacherRepositories.GetTeacherList());
        }

        public async Task<TeacherViewModel?> GetTeacherById(int id)
        {
            var teacher = await _unitOfWork.TeacherRepositories.GetByIdNoTracking(id);
            if (teacher != null)
            {
                var teach = _mapper.Map<TeacherViewModel>(teacher);
                return teach;
            }
            return null;
        }

        public async Task<bool> IsExistByEmail(string email)
        {
            var acc = await _unitOfWork.TeacherRepositories.GetByEmail(email);
            if (acc != null) return true;
            return false;
        }

        public async Task Create(CreateNewTeacher newTea)
        {
            if (newTea == null)
                throw new ArgumentNullException(nameof(newTea), "The new teacher info cannot be null.");

            var tea = new Teacher();
            _mapper.Map(newTea, tea);
            tea.Account.RoleId = await _unitOfWork.RoleRepositories.GetRoleIdByRoleName(CEGConstants.ACCOUNT_ROLE_TEACHER);

            // Save to the database
            try
            {
                _unitOfWork.TeacherRepositories.Create(tea);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating teacher account.", ex);
            }
            /*if (certImage != null && certImage.Length > 0)
            {
                string imageUrl = await _storageService.UploadToBlobAsync(certImage, "certificate/");
                acc.Certificate = imageUrl;
            }
            else
            {
                acc.Certificate = "";
            }*/
        }



        public void Update(TeacherViewModel teacher)
        {
            var acc = _mapper.Map<Teacher>(teacher);
            _unitOfWork.TeacherRepositories.Update(acc);
            _unitOfWork.Save();
        }

        public async Task<List<GetTeacherNameOption>?> GetTeacherNameOptionList()
        {
            return _mapper.Map<List<GetTeacherNameOption>>(await _unitOfWork.TeacherRepositories.GetTeacherNameOptionList());
        }

        public async Task<bool> IsExistByFullname(string fullname)
        {
            var acc = await _unitOfWork.TeacherRepositories.GetByFullname(fullname);
            return acc != null;
        }

        public async Task<bool> IsExistById(int id)
        {
            var acc = await _unitOfWork.TeacherRepositories.GetByIdNoTracking(id);
            return acc != null;
        }

        public async Task<TeacherViewModel?> GetByAccountId(int id)
        {
            var teacher = await _unitOfWork.TeacherRepositories.GetByAccountIdNoTracking(id);
            return teacher != null ? _mapper.Map<TeacherViewModel>(teacher) : null;
        }
        public async Task<List<GetStudentActivity>> GetStudentActivityListByScheduleId(int schId)
        {
            var stuActList = _mapper.Map<List<GetStudentActivity>>(await _unitOfWork.AttendanceRepositories.GetListByScheduleIdNoTracking(schId));
            List<int> homIds = await _unitOfWork.HomeworkRepositories.GetIdListByScheduleId(schId);
            var stuProList = _mapper.Map<List<StudentProgressViewModel>>(await _unitOfWork.StudentProgressRepositories.GetListByMultipleHomeworkId(homIds.ToArray()));
            foreach (var stuAct in stuActList)
            {
                stuAct.HomeworkAmount = homIds.Count;
                if(stuProList.Any(stuPro => stuPro.StudentId == stuAct.StudentId))
                {
                    stuAct.StudentProgress = stuProList.Where(stuPro => stuPro.StudentId == stuAct.StudentId).FirstOrDefault();
                    if(stuAct.StudentProgress != null)
                    {
                        stuAct.HomeworkCurrentProgress = stuAct.StudentProgress.StudentHomeworks
                            .Where(stuHom => homIds.Contains(stuHom.HomeworkId) && stuHom.Status == CEGConstants.STUDENT_HOMEWORK_STATUS_SUBMITTED)
                            .Count();
                        if(stuAct.StudentProgress.StudentHomeworks.Count > 0)
                        {
                            for (int i = 0; i < stuAct.StudentProgress.StudentHomeworks.Count; i++)
                            {
                                stuAct.StudentProgress.StudentHomeworks[i].Homework.HomeworkNumber = i + 1;
                            }
                        }
                    }
                }
            }
            return stuActList;
        }
        public async Task UploadToBlobAsync(string teacherName, IFormFile certificate, string imageType)
        {
            var tea = await _unitOfWork.TeacherRepositories.GetByFullname(teacherName) ??
                throw new ArgumentNullException(teacherName, "Teacher goes by fullname not found.");

            // Injected BlobServiceClient
            var blobContainerClient = _storageService.GetBlobContainerClient();
            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            // Generate a unique file name
            string fileName = Guid.NewGuid() + "-" + Path.GetExtension(certificate.FileName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            // Upload the file to Blob Storage
            using (var stream = certificate.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            if(imageType.Equals(CEGConstants.TEACHER_IMAGE_CERTIFICATE_TYPE))
                tea.Certificate = blobClient.Uri.ToString();

            // Save to the database
            try
            {
                _unitOfWork.TeacherRepositories.Update(tea);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while saving teacher certificate url link.", ex);
            }
        }
    }
}

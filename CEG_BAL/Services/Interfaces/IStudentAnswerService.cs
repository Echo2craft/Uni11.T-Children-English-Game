﻿using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Admin.Create;

namespace CEG_BAL.Services.Interfaces
{
    public interface IStudentAnswerService
    {
        void Create(CreateNewStudentAnswer newStuAns);
        void Update(UpdateStudentAnswer upStuAns);
        Task<List<StudentAnswerViewModel>> GetList();
        Task<StudentAnswerViewModel?> GetById(int id);
    }
}

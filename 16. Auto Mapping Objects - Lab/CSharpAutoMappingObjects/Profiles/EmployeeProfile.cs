using AutoMapper;
using CSharpAutoMappingObjects.DTOs;
using CSharpAutoMappingObjects.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAutoMappingObjects.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}

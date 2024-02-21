﻿using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Services
{
    public class CompanyService(ICompanyRepository baseRepository) : BaseService<Company, long>(baseRepository), ICompanyService
    {
    }
}
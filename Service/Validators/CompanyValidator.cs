using FluentValidation;
using IntegraHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Validators
{
    public class CompanyValidator: AbstractValidator<Company>
    {   
        public CompanyValidator() {
            
        }
    }
}

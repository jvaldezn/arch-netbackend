﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dto;
using FluentValidation;

namespace Data.Util.Validation
{
    public class LogDtoValidator : AbstractValidator<LogDto>
    {
        public LogDtoValidator()
        {
            RuleFor(x => x.MachineName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("No se ha ingresado el hostname");

            RuleFor(x => x.Logged)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("No se ha indicado la fecha del loggeo");

            RuleFor(x => x.Message)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("No se el mensaje del log");

            RuleFor(x => x.Logger)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("No se ha indicado quien graba el log");

            RuleFor(x => x.ApplicationId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("No se ha indicado a que aplicación pertenece el log");
        }
    }
}

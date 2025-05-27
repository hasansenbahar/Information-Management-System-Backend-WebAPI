﻿using FluentValidation;
using WebService.API.Data.Entity;
using WebService.API.Models.PersonModels;

namespace WebService.API.Validations
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePerson>
    {
        public UpdatePersonValidator()
        {
            // Name - MinLength 10, MaxLength 250
            //RuleFor(x => x.Name)
            //    .MinimumLength(10)
            //    .WithMessage("The 'Title' should have at least 10 characters.")
            //    .MaximumLength(250)
            //    .WithMessage("The 'Title' should have not more than 250 characters.");
        }

        //examples
        //public CustomerValidator()
        //{
        //    RuleFor(x => x.Surname).NotEmpty();
        //    RuleFor(x => x.Forename).NotEmpty().WithMessage("Please specify a first name");
        //    RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
        //    RuleFor(x => x.Address).Length(20, 250);
        //    RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        //}

        //private bool BeAValidPostcode(string postcode)
        //{
        //    // custom postcode validating logic goes here
        //}


        public class CreatePersonValidator : AbstractValidator<CreatePerson>
        {
            public CreatePersonValidator()
            {
                //RuleFor(x => x.Name)
                //    .MinimumLength(10)
                //    .WithMessage("The 'Title' should have at least 10 characters.")
                //    .MaximumLength(250)
                //    .WithMessage("The 'Title' should have not more than 250 characters.");
            }


        }
    }
}

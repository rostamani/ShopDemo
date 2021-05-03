﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public string Picture { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Heading { get;  set; }

        public string Title { get;  set; }
        public string Text { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Link { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string BtnText { get;  set; }

    }
}

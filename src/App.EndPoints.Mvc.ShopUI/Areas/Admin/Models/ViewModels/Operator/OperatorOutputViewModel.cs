﻿using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.Mvc.AdminUI.Models.ViewModels.Operator
{
    public class OperatorOutputViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [Display(Name = "کد رنگ")]
        public string Code { get; set; } = String.Empty;

        [Display(Name = "نام")]
        public string Name { get; set; } = String.Empty;
        [Display(Name = "حذف شده")]
        public bool IsDeleted { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreationDate { get; set; }
    }
}

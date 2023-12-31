﻿using System.ComponentModel.DataAnnotations;
using Dal.Categories.Entity;
using Dal.Message.Entity;
using Dal.Operation.Entity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;

namespace Dal.User.Entity;

public class UserDal : IdentityUser
{
    public decimal? Balance { get; set; } = 0;
    
    [MaxLength(255)]
    public string Name { get; set; }

    public bool CheckExistenceMail { get; set; }
    public string? PathToImg { get; set; }
    public string? RefreshToken { get; set; }

    public List<CategoriesDal>? CategoriesList { get; set; }
    public List<MessageDal>? MessageList { get; set; }
    
    public List<OperationDal>? OperationList { get; set; }
}
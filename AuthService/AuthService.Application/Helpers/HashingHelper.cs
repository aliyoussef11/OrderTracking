﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Helpers
{
    public static class HashingHelper
    {
        public static string HashPasswordWithSHA256(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            var hashedInput = HashPasswordWithSHA256(inputPassword);
            return hashedInput == storedHashedPassword;
        }
    }
}

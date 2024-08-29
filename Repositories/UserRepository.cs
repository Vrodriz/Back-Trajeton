namespace ApiAuth.Repositories
{
    using ApiAuth.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class UserRepository
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Password = "Cleancode10*", Email = "vrernas2@gmail.com" },
            new User { Id = 2, Password = "Anaclara*", Email = "thaligato@gmail.com" }
        };

        public static User Get(string email, string password)
        {
            return users.FirstOrDefault(user =>
                user.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)
                && user.Password == password);
        }

        public static User GetByEmail(string email)
        {
            return users.FirstOrDefault(user =>
                user.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }

        public static bool UpdatePassword(string email, string newPassword)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return false; // Usuário não encontrado
            }

            user.Password = newPassword;
            return true; // Senha atualizada com sucesso
        }
    }
}

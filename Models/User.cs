using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class UserChunk
    {
        [Key]
        public long userId { get; set; }

        public string email { get; set; }
    }

    public class UserMain
    {
        public long userId { get; set; }
        public UserChunk UserChunk { get; set; }

        public long totalLikes { get; set; }

        public long totalPosts { get; set; }

        public long totalFollowers { get; set; }

        public string handle { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string bio { get; set; }
    }

    public class ExtendedUser
    {
        public long userId { get; set; }
        public UserChunk UserChunk { get; set; }

        public string password { get; set; }

        public string phone { get; set; }

        public DateTime birthday { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public static string hashPassword(string rawPassword)
        {
            // TODO
            return rawPassword;
        }
    }

    public class UserModel
    {
        public UserChunk UserChunk { get; set; }

        public UserMain UserMain { get; set; }

        public ExtendedUser ExtendedUser { get; set; }

        public static bool userIsValid(UserModel userModel)
        {
            // TODO            
            return true;
        }
    }
}

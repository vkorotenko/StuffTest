using StuffTest.Model;
using System;

namespace StuffTest.Data
{
    public class SeedData
    {
        public static Position[] Positions = new[]
        {

            new Position{Name = "Юрист" ,

                Id = Guid.Parse("{75EAD236-3477-48FA-B509-820FC517F198}"),
                },
            new Position{Name = "Программист" ,

                Id = Guid.Parse("{B179C04D-D55C-4A41-AE1B-DFC81A53B9CC}"),
                },
            new Position{Name = "Маркетолог" ,

                Id = Guid.Parse("{9CB184DF-AC44-4A92-97BF-5540CBFF0B6F}"),
                },
        };
        public static User[] Users = new[]
       {

            new User{FirstName = "Ксения" ,
                LastName = "Умывакина",
                MiddleName ="Викторовна",
                PositionId = Guid.Parse("{75EAD236-3477-48FA-B509-820FC517F198}"),
                Id = Guid.Parse("{3FAF8C38-697B-4491-AB15-14A0AC3CA7EE}"),
                },
            new User{FirstName = "Владимир" ,
                LastName= "Коротенко",
                MiddleName= "Николаевич",
                PositionId = Guid.Parse("{B179C04D-D55C-4A41-AE1B-DFC81A53B9CC}"),
                Id = Guid.Parse("{2405FB3B-1F0C-46D7-95C9-F20974D19779}"),
                },
            new User{FirstName = "Елизавета" ,
                LastName ="Терентьева",
                MiddleName = "Викторовна",
                PositionId = Guid.Parse("{9CB184DF-AC44-4A92-97BF-5540CBFF0B6F}"),
                Id = Guid.Parse("{0A2BBD41-A9C2-4AA7-8288-8E1DD2C9B4C2}"),
                },
        };
    }
}

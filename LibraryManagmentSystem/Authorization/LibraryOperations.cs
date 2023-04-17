using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace LibraryManagmentSystem.Authorization
{
    public class LibraryOperations
    {

        public static OperationAuthorizationRequirement Activate =
            new OperationAuthorizationRequirement { Name = Constants.ActivateOperationName };

        public static OperationAuthorizationRequirement Block =
            new OperationAuthorizationRequirement { Name = Constants.BlockOperationName };
    }

    public class Constants
    {

        public static readonly string ActivateOperationName = "Activate";
        public static readonly string BlockOperationName = "Block";

        public static readonly string ManagerRole = "Menager";
        public static readonly string AdminRole = "Admin";
    }
}

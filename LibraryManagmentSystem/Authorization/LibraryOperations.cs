using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace LibraryManagmentSystem.Authorization
{
    public class LibraryOperations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement{ Name = Constants.CreateOperationName };

        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = Constants.UpddateOperationName };

        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        public static OperationAuthorizationRequirement Issue =
            new OperationAuthorizationRequirement { Name = Constants.IssueOperationName };

        public static OperationAuthorizationRequirement Return =
            new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

        public static OperationAuthorizationRequirement Activate =
            new OperationAuthorizationRequirement { Name = Constants.ActivateOperationName };

        public static OperationAuthorizationRequirement Block =
            new OperationAuthorizationRequirement { Name = Constants.BlockOperationName };
    }

    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string UpddateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";

        public static readonly string IssueOperationName = "Issue";
        public static readonly string ReturnOperationName = "Return";

        public static readonly string ActivateOperationName = "Activate";
        public static readonly string BlockOperationName = "Block";

        public static readonly string ManagerRole = "Menager";
    }
}

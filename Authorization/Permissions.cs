namespace WebService.API.Authorization
{
    public static class Permissions
    {
        //Todo
        public const string Todo_View = "Permissions.Todo.View";
        public const string Todo_Create = "Permissions.Todo.Create";
        public const string Todo_Edit = "Permissions.Todo.Edit";
        public const string Todo_Delete = "Permissions.Todo.Delete";
        public const string Todo_ViewById = "Permissions.Todo.ViewById";
        public const string Todo_Exists = "Permissions.Todo.Exists";

        //Allocation
        public const string Allocation_View = "Permissions.Allocation.View";
        public const string Allocation_Create = "Permissions.Allocation.Create";
        public const string Allocation_Edit = "Permissions.Allocation.Edit";
        public const string Allocation_Delete = "Permissions.Allocation.Delete";
        public const string Allocation_ViewById = "Permissions.Allocation.ViewById";
        public const string Allocation_Exists = "Permissions.Allocation.Exists";

        //Person
        public const string Person_View = "Permissions.Person.View";
        public const string Person_Create = "Permissions.Person.Create";
        public const string Person_Edit = "Permissions.Person.Edit";
        public const string Person_Delete = "Permissions.Person.Delete";
        public const string Person_ViewById = "Permissions.Person.ViewById";
        public const string Person_Exists = "Permissions.Person.Exists";

        //Users
        public const string Users_View = "Permissions.Users.View";
        public const string Users_Create = "Permissions.Users.Create";
        public const string Users_Edit = "Permissions.Users.Edit";
        public const string Users_Delete = "Permissions.Users.Delete";
        public const string Users_ViewById = "Permissions.Users.ViewById";
        public const string Users_Role_Create = "Permissions.Users.CreateRole";
        public const string Users_Role_Delete = "Permissions.Users.DeleteRole";
        public const string Users_Exists = "Permissions.Users.Exists";
        
        //Roles
        public const string Roles_View = "Permissions.Roles.View";
        public const string Roles_Create = "Permissions.Roles.Create";
        public const string Roles_Edit = "Permissions.Roles.Edit";
        public const string Roles_Delete = "Permissions.Roles.Delete";
        public const string Roles_ViewById = "Permissions.Roles.ViewById";

        //Permissions
        public const string Permissions_View = "Permissions.Permissions.View";
        public const string Permissions_Update = "Permissions.Permissions.Edit";
        

        //for initial if you want another permission to add on initial you can extend below
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

    }
}

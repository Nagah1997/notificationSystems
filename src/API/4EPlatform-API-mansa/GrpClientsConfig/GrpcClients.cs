



namespace _4EPlatform_API_Template.GrpClientsConfig
{
    public static class GrpcClients
    {


        public static void AddUserManagementGrpcClients(
            this IServiceCollection services,
            string UserManagmentUrl)
        {

            services.AddGrpcServiceClient<UserProtoService.UserProtoServiceClient>(UserManagmentUrl);
        }

     
      
      
       
     
        public static void AddBlogGrpcClients(this IServiceCollection services, string BlogManagmentUrl)
        {
          
        }

    }
}

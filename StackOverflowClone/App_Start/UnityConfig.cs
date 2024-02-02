using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using StackOverflowClone.ServiceLayer;
using System.Web.Mvc;

namespace StackOverflowClone
{
	public static class UnityConfig
	{
		public static void RegisterComponents()

			//Sử dụng Unity Container để quản lý việc tạo và kết nối các phần ứng dụng, giải quyết dependency injection, giúp việc phát triển và bảo trì mã nguồn dễ dàng hơn.
		{
			var container = new UnityContainer();

			//khi có yêu cầu một đối tượng của IQuestionsService, nó nên tạo ra một đối tượng của lớp QuestionsService.
			container.RegisterType<IQuestionsService, QuestionsService>();
			container.RegisterType<IUsersService, UsersService>();
			container.RegisterType<ICategoriesService, CategoriesService>();

			DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
		}
	}
}
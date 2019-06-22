using AspNetCoreApiExample.Data.VO;

namespace AspNetCoreApiExample.Business
{
    public interface IUserBusiness
    {
        object FindByLogin(UserVO user);

    }
}

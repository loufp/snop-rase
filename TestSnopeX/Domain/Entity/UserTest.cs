using snoperase.Domain.Entites;

namespace TestSnopeX.Domain.Entity;

public class UserTest
{
    public void Constructor_SetsAllProperties()
    {
        var id = new Guid();

        var user = new User(id,"Kirill","mlvfdm","123321");
        
    }
}
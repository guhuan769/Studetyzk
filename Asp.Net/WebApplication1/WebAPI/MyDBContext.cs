using WebAPI.Model;

namespace WebAPI
{
    public class MyDBContext
    {
        public static Task<Person?> GetByIdAsync(long id)
        {
            var result = GetById(id);
            return Task.FromResult(result);
        }
        public static Person? GetById(long id)
        {
            switch (id)
            {
                case 0:
                    return new Person(1,"顾欢",24);
                case 1:
                    return new Person(2, "顾欢2", 25);
                default:
                    return null;
            }
        }
    }
}

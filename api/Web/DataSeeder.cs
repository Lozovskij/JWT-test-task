using Core.Models;
using DataAccess;

namespace Web;

public static class DataSeeder
{
    public static void Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.EnsureCreated();
        AddRequests(context);
    }

    private static void AddRequests(DataContext context)
    {
        var request = context.Requests.FirstOrDefault();
        if (request != null) return;

        var requests = new List<Request>()
        {
            new() { Description = "Ремонт или замена сломанного компьютера" },
            new() { Description = "Ремонт принтера или сканера" },
            new() { Description = "Замена неисправной клавиатуры, мыши и другой периферии" },
            new() { Description = "Ремонт стула, стола, устранение скрипов" },
            new() { Description = "Ремонт диспенсера для воды" },
            new() { Description = "Мерцающий или неработающий офисный свет" },
            new() { Description = "Помощь при проблемах с подключением к сети" },
            new() { Description = "Установка программного обеспечения или обновлений" },
            new() { Description = "Помощь с восстановлением файлов или получением данных" },
            new() { Description = "Замена утерянной или поврежденной карты доступа" },
            new() { Description = "Калибровка монитора" },

        };
        context.Requests.AddRange(requests);
        context.SaveChanges();
    }
}
using Flunt.Notifications;
using Flunt.Validations;

namespace Service.Models
{
    public class Arquivo : Notifiable, IValidatable
    {
        public string Texto { get; set; }
        public string Key { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(Texto, "Texto", "O campo texto nao pode ser vazio")
                .IsNotNullOrWhiteSpace(Key, "Key", "O campo key é obrigatório"));
        }
    }
}

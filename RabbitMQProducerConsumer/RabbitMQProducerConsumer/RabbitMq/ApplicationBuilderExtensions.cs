namespace RabbitMQProducerConsumer.RabbitMq;


    public static class ApplicationBuilderExtentions
    {
        
        private  static Consumer Listener = new();

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<Consumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            return app;
        }

        private static void OnStarted()
        {
            Listener.Register();
        }
        
    }

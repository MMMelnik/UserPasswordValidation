namespace client
{
    class GenerateProxy
    {
        [System.ServiceModel.ServiceContractAttribute(Namespace = "http://UserPasswordValidation")]
        public interface ICalculator
        {

            [System.ServiceModel.OperationContractAttribute(Action = "http://UserPasswordValidation/ICalculator/Add", ReplyAction = "http://UserPasswordValidation/ICalculator/AddResponse")]
            double Add(double n1, double n2);

            [System.ServiceModel.OperationContractAttribute(Action = "http://UserPasswordValidation/ICalculator/Subtract", ReplyAction = "http://UserPasswordValidation/ICalculator/SubtractResponse")]
            double Subtract(double n1, double n2);

            [System.ServiceModel.OperationContractAttribute(Action = "http://UserPasswordValidation/ICalculator/Multiply", ReplyAction = "http://UserPasswordValidation/ICalculator/MultiplyResponse")]
            double Multiply(double n1, double n2);

            [System.ServiceModel.OperationContractAttribute(Action = "http://UserPasswordValidation/ICalculator/Divide", ReplyAction = "http://UserPasswordValidation/ICalculator/DivideResponse")]
            double Divide(double n1, double n2);
        }

        public interface ICalculatorChannel : ICalculator, System.ServiceModel.IClientChannel
        {
        }

        public class CalculatorProxy : System.ServiceModel.ClientBase<ICalculator>, ICalculator
        {

            public CalculatorProxy()
            {
            }

            public CalculatorProxy(string endpointConfigurationName)
                :
                    base(endpointConfigurationName)
            {
            }

            public CalculatorProxy(string endpointConfigurationName, string remoteAddress)
                :
                    base(endpointConfigurationName, remoteAddress)
            {
            }

            public CalculatorProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress)
                :
                    base(endpointConfigurationName, remoteAddress)
            {
            }

            public CalculatorProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
                :
                    base(binding, remoteAddress)
            {
            }

            public double Add(double n1, double n2)
            {
                return Channel.Add(n1, n2);
            }

            public double Subtract(double n1, double n2)
            {
                return Channel.Subtract(n1, n2);
            }

            public double Multiply(double n1, double n2)
            {
                return Channel.Multiply(n1, n2);
            }

            public double Divide(double n1, double n2)
            {
                return Channel.Divide(n1, n2);
            }
        }
    }
}
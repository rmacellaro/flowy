namespace It.Flowy.Engine.Models.Exceptions;

public class ProcessingException : Exception {
    public string Code { get; private set; }

    public ProcessingException(string code, string message): base(message) {
        Code = code;
    }

    public ProcessingException(string code, string message, Exception ex): base(message, ex) {
        Code = code;
    }
}
namespace RentAThing.Server.Application.Exceptions {

    public class RentException(string msg, Exception? inner = null) : Exception(msg, inner) {
    }

    public class RentAlreadyStartedException(string msg, Exception? inner = null) : RentException(msg, inner) {
    }

}

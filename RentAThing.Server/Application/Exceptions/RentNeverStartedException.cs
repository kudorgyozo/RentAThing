namespace RentAThing.Server.Application.Exceptions {
    [Serializable]
    internal class RentNeverStartedException : Exception {
        public RentNeverStartedException() {
        }

        public RentNeverStartedException(string? message) : base(message) {
        }

        public RentNeverStartedException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
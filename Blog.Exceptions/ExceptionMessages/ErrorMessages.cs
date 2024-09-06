namespace Blog.Exceptions.ExceptionMessages;

public abstract class ErrorMessages {
    public const string EmptyUsername = "The 'username' field cannot be empty.";
    public const string EmptyEmail = "The 'email' field cannot be empty.";
    public const string InvalidEmail = "The email address provided is not valid.";
    public const string EmailExists = "This email already has an account associated with it.";
    public const string InvalidPassword = "The password provided is not valid.";
    public const string InvalidPasswordOrEmail = "Invalid email or password. Please try again.";
}
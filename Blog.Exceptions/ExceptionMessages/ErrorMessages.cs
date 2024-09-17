namespace Blog.Exceptions.ExceptionMessages;

public abstract class ErrorMessages {

    #region User Errors

    public const string EmptyUsername = "The 'username' field cannot be empty.";
    public const string EmptyEmail = "The 'email' field cannot be empty.";
    public const string InvalidEmail = "The email address provided is not valid.";
    public const string EmailExists = "This email already has an account associated with it.";
    public const string InvalidPassword = "The password provided is not valid.";
    public const string InvalidPasswordOrEmail = "Invalid email or password. Please try again.";
    public const string UserNotFound = "The provided user does not exist.";

    #endregion

    #region Post Errors

    public const string NoUserAssociated = "There must be atleast one user associated with this post.";
    public const string EmptyTitle = "The 'title' field cannot be empty.";
    public const string EmptyTextContent = "The 'textContent' field cannot be empty.";
    public const string PostWithTitleAlreadyExists = "You already have a post with this title.";

    #endregion

}
using Core.Models.Manager.Exception;

namespace Tools.JWT.Helper.Exceptions;

internal class RetrievingUserClaimsException : ExceptionArgs
{
    public override string Message => base.Message + "Fail to retrieve user claims.";
    public override string DescriptiveStringCode => "FAIL_TO_RETRIVE_USER_CLAIMS";
    public override int ErrorCode => 404;
}
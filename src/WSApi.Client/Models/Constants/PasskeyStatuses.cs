namespace WSApi.Client.Models.Constants;

/// <summary>
/// Values of <c>PasskeyState.Status</c> and <c>SessionStatus.PasskeyStatus</c>.
/// </summary>
public class PasskeyStatuses
{
    /// <summary>No passkey step in progress.</summary>
    public const string None = "none";

    /// <summary>WhatsApp is waiting for the passkey assertion; <c>Challenge</c> and <c>Origin</c> are set.</summary>
    public const string Required = "required";

    /// <summary>The assertion was sent; waiting for WhatsApp to complete the link.</summary>
    public const string Submitted = "submitted";

    /// <summary>Show <c>ConfirmationCode</c> to the user so they can compare it and approve on their phone.</summary>
    public const string ConfirmationRequired = "confirmation_required";

    /// <summary>The step failed or expired; see <c>Error</c>.</summary>
    public const string Error = "error";

    /// <summary>
    /// Returns true while a passkey step is pending (<c>required</c>, <c>submitted</c> or
    /// <c>confirmation_required</c>). While pending, stop polling the QR / pair-code endpoints.
    /// </summary>
    public static bool IsPending(string? status) =>
        status is Required or Submitted or ConfirmationRequired;
}

/// <summary>
/// Values of <c>PasskeyState.Source</c>.
/// </summary>
public class PasskeySources
{
    /// <summary>WhatsApp pushed the challenge after the QR scan or pair code.</summary>
    public const string Notification = "notification";

    /// <summary>The challenge was fetched via <c>POST /session/passkey/request</c>.</summary>
    public const string Requested = "requested";
}

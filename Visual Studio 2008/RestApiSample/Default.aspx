<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RESTAPISample.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Rest API Samples</title>
    <style type="text/css">
        .imagelink
        {
            padding: 5px 0px 5px 28px;
        }
        .execute
        {
            background: url(  'Images/play_button.png' ) no-repeat left top;
        }
        .source
        {
            background: url(  'Images/edt-format-source-button.png' ) no-repeat left top;
        }
    </style>
</head>
<body>
    <center>
        <h3>
            REST API Samples</h3>
    </center>
    <br />
    <br />
    <table cellspacing="5" width="85%">
        <tbody>
        
            <tr valign="top">
                <td>
                    Payment with a credit card
                </td>
                <td>
                </td>
                <td width="30%">
                    <a href="PaymentWithCreditCard.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td width="30%">
                    <a href="Source/PaymentWithCreditCard.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Payment with a PayPal Account
                </td>
                <td>
                </td>
                <td>
                    <a href="PaymentWithPayPal.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/PaymentWithPayPal.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Get Payment Details
                </td>
                <td>
                </td>
                <td>
                    <a href="GetPayment.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetPayment.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Get Payment History
                </td>
                <td>
                </td>
                <td>
                    <a href="GetPaymentHistory.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetPaymentHistory.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Get Sale Details
                </td>
                <td>
                </td>
                <td>
                    <a href="GetSale.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetSale.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Refund a Payment
                </td>
                <td>
                </td>
                <td>
                    <a href="SaleRefund.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/SaleRefund.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Save a credit card
                </td>
                <td>
                </td>
                <td>
                    <a href="CreateCreditCard.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/CreateCreditCard.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Get credit card Details
                </td>
                <td>
                </td>
                <td>
                    <a href="GetCreditCard.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetCreditCard.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Payment with saved credit card
                </td>
                <td>
                </td>
                <td>
                    <a href="PaymentWithSavedCard.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/PaymentWithSavedCard.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Get Authorization
                </td>
                <td>
                </td>
                <td>
                    <a href="GetAuthorization.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetAuthorization.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Capture Authorization
                </td>
                <td>
                </td>
                <td>
                    <a href="AuthorizationCapture.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/AuthorizationCapture.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Void Authorization
                </td>
                <td>
                </td>
                <td>
                    <a href="AuthorizationVoid.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/AuthorizationVoid.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    Reauthorization
                </td>
                <td>
                </td>
                <td width="30%">
                    <a href="Reauthorization.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td width="30%">
                    <a href="Source/Reauthorization.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Get Capture
                </td>
                <td>
                </td>
                <td>
                    <a href="GetCapture.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetCapture.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr>
                <td>
                    Refund Capture
                </td>
                <td>
                </td>
                <td>
                    <a href="RefundCapture.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/RefundCapture.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>

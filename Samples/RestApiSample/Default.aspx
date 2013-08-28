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
        .header {
		font-weight: bold;
	    }
	    .header td {
		    padding: 10px 0px 10px 0px;
	    }
    </style>
</head>
<body>
    <center>
        <h3>
            PayPal REST API Samples</h3>
    </center>
    
    <table cellspacing="5" width="85%">
        <tbody>
            <tr valign="top" class="header">
				<td>Payments</td>
			</tr>
            <tr valign="top">
                <td>
                    Direct credit card payments
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
                   PayPal account payments
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
                    Stored credit card payments
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
                    Get payment details
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
                    Get payment history
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
            <tr valign="top" class="header">
				<td>Sale</td>
			</tr>
            <tr>
                <td>
                    Get sale payment details
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
                    Refund a sale payment
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
            <tr valign="top" class="header">
				<td>Refund</td>
			</tr>
            <tr>
                <td>
                    Get refund details
                </td>
                <td>
                </td>
                <td>
                    <a href="GetRefund.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/GetRefund.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr valign="top" class='header'>
				<td>Vault</td>
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
                    Get credit card details
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
                    Delete a credit card 
                </td>
                <td>
                </td>
                <td>
                    <a href="DeleteCreditCard.aspx" class="execute imagelink">Execute</a>
                </td>
                <td>
                </td>
                <td>
                    <a href="Source/DeleteCreditCard.aspx.html" class="source imagelink">Source</a>
                </td>
            </tr>
            <tr valign="top" class='header'>
				<td>Authorization</td>
			</tr>
            <tr>
                <td>
                    Get an authorized payment details
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
                    Capture an authorized payment
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
                    Void an authorized payment
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
                    Reauthorize an authorized payment
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
            <tr valign="top" class="header">
				<td>Capture</td>
			</tr>
            <tr>
                <td>
                    Get captured payment details
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
                    Refund captured payment
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

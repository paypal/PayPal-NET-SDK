<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="RESTAPISample.Response" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Response Page</title>
    
</head>
<body>
    <form id="form1" runat="server">
		 <a href="../">Back</a><br/>
		  <%if (RedirectURL != null)
          { %><a href= '<%=RedirectURL%>'>Redirect
		to PayPal to approve the payment</a>
             <br />
        <%} %>
       
         <table border="1px" style="border-collapse: collapse">
		<tr>
			<th>Request</th>
			<th>Response</th>
		</tr>
		<tr>
			<td valign="top"><pre id="request">  <%if (RequestMessage != null)
          { %>
             <%=RequestMessage %>
        <%} %>
         <%else
          { %>
             No payload for this request
        <%} %>
					
			</pre></td>
			<td valign="top"><pre id="response">
					 <%if (ResponseMessage != null)
          { %>
             <%=ResponseMessage %>
        <%} %>
         <%else if (ErrorMessage != null)
          { %>
             <%=ErrorMessage %>
        <%} %>
				</pre></td>
		</tr>
	</table>
        
        <br />
        
        <br />
        
        <a href="../">Back</a>
    </div>
    </form>
</body>
</html>

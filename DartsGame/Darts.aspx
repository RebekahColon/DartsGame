<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Darts.aspx.cs" Inherits="DartsGame.Darts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Darts Game</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divGameSetup" class="container" runat="server">
            <div class="page-header col-sm-8">
                <h2>Welcome to the Dart Games</h2>
                <h3>Fill out the form below to begin.</h3>
            </div>
            <div class="col-sm-4">
                <img src="/Assets/DartBoardRound.jpg" class="img-responsive" alt="Classic Dartboard" width="200" height="175" />
            </div>
            <div class="col-sm-8">
                <div class="form-group">
                    <label for="ddlWinningThreshold">Select the winning threshold:</label>
                    <asp:DropDownList ID="ddlWinningThreshold" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWinningThreshold_SelectedIndexChanged">
                        <asp:ListItem>Select One</asp:ListItem>
                        <asp:ListItem>Maximum Rounds Reached</asp:ListItem>
                        <asp:ListItem>Maximum Points Reached</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="divEnterMaxData" runat="server" visible="false">
                    <div id="divMaxRounds" class="form-group" runat="server">
                        <label>Enter the maximum rounds:</label>
                        <asp:TextBox ID="txtMaxRounds" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div id="divMaxPoints" class="form-group" runat="server">
                        <label>Enter the maximum points:</label>
                        <asp:TextBox ID="txtMaxPoints" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label>Enter the names of each Player:</label>
                    <asp:TextBox ID="txtPlayer1" runat="server" CssClass="form-control verticalSpace"></asp:TextBox>
                    <asp:TextBox ID="txtPlayer2" runat="server" CssClass="form-control verticalSpace"></asp:TextBox>
                    <asp:TextBox ID="txtPlayer3" runat="server" CssClass="form-control verticalSpace"></asp:TextBox>
                    <asp:TextBox ID="txtPlayer4" runat="server" CssClass="form-control verticalSpace"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnStartGame" runat="server" CssClass="btn btn-primary" Text="Start Game" OnClick="btnStartGame_Click" />
                </div>
                <div id="divErrors" class="alert alert-danger" runat="server" visible="false">
                    <strong>Oops!</strong> Looks like something went wrong.
                    <asp:Label ID="lblErrorInfo" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <div id="divGameResults" class="container" runat="server" visible="false">
            <div class="page-header col-sm-8">
                <h2>Game Results</h2>
                <h3>Winner: <b>
                    <asp:Literal ID="lblWinnerName" runat="server"></asp:Literal></b></h3>
            </div>
            <div class="col-sm-4">
                <img src="/Assets/DartBoardRound.jpg" class="img-responsive" alt="Classic Dartboard" width="125" height="100" />
            </div>
            <% foreach (var result in Results)
                { %>
            <div class="col-sm-5">
                <h4><% =result.Player.Name %>. Final Score: <b><% =result.Player.Score %></b></h4>
                <table class="table table-responsive table-bordered">
                    <thead>
                        <tr>
                            <th>Throw</th>
                            <th>Board Location</th>
                            <th>Points</th>
                        </tr>
                    </thead>
                    <% foreach (var dartScore in result.DartScoreViewModels)
                        { %>
                    <tr>
                        <td><%=dartScore.Throw %></td>
                        <td><%=dartScore.BoardPosition %></td>
                        <td><%=dartScore.Points %></td>
                    </tr>
                    <% } %>
                </table>
            </div>
            <% } %>
            <div class="form-group col-sm-12">
                <asp:Button ID="btnPlayAgain" runat="server" CssClass="btn btn-primary" Text="Play Again" OnClick="btnPlayAgain_Click" />
                <asp:Button ID="btnStartNewGame" runat="server" CssClass="btn btn-primary" Text="Start Over" OnClick="btnStartOver_Click" />
            </div>
        </div>
    </form>
</body>
</html>

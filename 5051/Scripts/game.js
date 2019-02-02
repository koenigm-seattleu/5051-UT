/// <reference path ="../scripts/jquery/index.d.ts"/>
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
/*
 *
 * Global Variables
 *
 */
// Track the Current Iteration.  
// Use the Iteraction Number to determine, if new data refresh is needed, a change in number means yes, refresh data
// Start at -1, so first time run, always feteches data.
var CurrentIterationNumber = -1;
// Hold the Server's Iteration
var ServerIterationNumber = 0;
// The Student Id is stored in the Dom, need to fetch it on page load
var StudentId = $("#StudentId").val();
// Refresh rate is the rate to refresh the game in miliseconds
var ServerRefreshRate = 1000;
// Game Update Timmer fires every RefeshRate
var GameUpdateTimer;
// The Global Data for the Current ShopData
var ShopData = {};
// The global path to the Shop Folder for Images
var BaseContentURL = "/Content/shop/";
var BaseContentAvatarURL = "/Content/avatar/";
// Empty Item
var EmptyItem = "placeholder.png";
/*
 *
 * Data Functions
 *
 */
// Parses the Data Structure and returns the Iteration Number
function DataLoadIterationNumber(data) {
    var IterationNumber = data.Data;
    return IterationNumber;
}
// Does a fetch to the server, and returns the Iteration Number
function GetIterationNumber() {
    return __awaiter(this, void 0, void 0, function* () {
        $.ajax("/Game/GetIterationNumber", {
            cache: false,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                // Update the Global Server Iteration Number
                ServerIterationNumber = DataLoadIterationNumber(data);
            },
            error: function (data) {
                alert("Fail GetIterationNumber");
            }
        })
            .fail(function () {
            console.log("Error Get Iteration Number");
        });
    });
}
// Parses the Data Structure and returns the Iteration Number
function DataLoadGameResults(data) {
    var result = data.Data;
    ServerIterationNumber = result.IterationNumber;
    ShopData.TopperUri = result.TopperUri;
    ShopData.MenuUri = result.MenuUri;
    ShopData.WheelsUri = result.WheelsUri;
    ShopData.SignUri = result.SignUri;
    ShopData.TruckUri = result.TruckUri;
    ShopData.TrailerUri = result.TrailerUri;
    ShopData.IsClosed = result.IsClosed;
    ShopData.CustomersTotal = result.CustomersTotal;
    ShopData.TransactionList = result.TransactionList;
    ShopData.Tokens = result.Tokens;
    ShopData.Experience = result.Experience;
    console.log(data.Data);
}
// Does a fetch to the server, and returns the Iteration Number
function GetGameResults() {
    var data = { "Id": StudentId.toString() };
    $.ajax({
        url: "/Game/GetResults/",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: 'json',
        type: 'POST',
        async: false,
        success: function (data) {
            DataLoadGameResults(data);
            return;
        },
        error: function (data) {
            alert("Fail GetResults");
        }
    })
        .fail(function () {
        console.log("Error GetResults");
        return;
    });
    return 0;
}
// Get the Refresh rate for the page
// Returns the number of miliseconds to refresh
function GetRefreshRate() {
    // Set the Global Refresh rate
    GetGameRefreshRate();
}
// Parses the Data Structure and returns the Iteration Number
function DataLoadRefreshRate(data) {
    var rate = data.Data;
    return rate;
}
// Does a fetch to the server, and returns the Iteration Number
function GetGameRefreshRate() {
    return __awaiter(this, void 0, void 0, function* () {
        yield $.ajax("/Game/GetRefreshRate/", {
            cache: false,
            dataType: 'json',
            type: 'POST',
            async: false,
            success: function (data) {
                ServerRefreshRate = DataLoadRefreshRate(data);
            },
            error: function (data) {
                alert("Fail GetRefreshRate");
            }
        })
            .fail(function () {
            console.log("Error Get Refresh Rate");
        });
    });
}
// Refresh the Game
function RefreshGame() {
    return __awaiter(this, void 0, void 0, function* () {
        // Force a call to Simulation
        yield GetIterationNumber();
        // Check if Game Version > current version, if so do update sequence
        if (ServerIterationNumber > CurrentIterationNumber) {
            UpdateGame();
        }
    });
}
function UpdateGame() {
    // Get New Data
    GetGameResults();
    // Refresh Game Display
    RefreshGameDisplay();
    // Update Iteration Number
    CurrentIterationNumber = ServerIterationNumber;
}
// Set the Default on Boot to draw, before the rest draws if no data exists
function SetDefaultShopData() {
    ShopData.TruckUri = "Truck0.png";
    ShopData.TopperUri = "Topper0.png";
    ShopData.MenuUri = "Menu0.png";
    ShopData.WheelsUri = "Wheels0.png";
    ShopData.SignUri = "Sign0.png";
    ShopData.TrailerUri = "Trailer0.png";
}
/*
 * Application Starts Here
 *
 * Load the Page
 * Initialize the Game
 * Do a fetch to the server to get IterationNumber, Refresh Rate
 *
 * Set Refresh Rate on a Timmer
 *
 * At the refresh go to the server and check for Iteration
 *
 * If Current Iteration < Server Iteration Number, then refetch Data
 *
 * Update Display for new Data
 *
 */
// Set the Default Data for before the data loads from the server
SetDefaultShopData();
DrawEmptyTruckItems();
// Call for Refresh Game to get the Initial State
UpdateGame();
// Then start looping to refresh every RefreshRate Iteration
// Get Refresh Rate
GetRefreshRate();
// Make Timmer to call refresh
setInterval(function () {
    RefreshGame();
}, ServerRefreshRate);
/*
 *
 * Game Layout Starts Here
 *
 *
 */
// Refresh Game display
function RefreshGameDisplay() {
    // Use the current data structure
    // For all the elements in the Game, make a call and refresh them
    // Show Iteration Number (debugging)
    $("#IterationNumber").text(CurrentIterationNumber);
    // Update Data
    $("#ExperienceValue").text(ShopData.Experience);
    $("#CustomersValue").text(ShopData.CustomersTotal);
    $("#TokensValue").text(ShopData.Tokens);
    // Show the Status of the Store
    var storeStatus = "Open for Business";
    if (ShopData.IsClosed) {
        storeStatus = "Closed, Rent is due";
    }
    $("#StoreStatus").text(storeStatus);
    ShowTransactionList();
    // Refesh Truck
    // If the Truck is Empty, remove all items
    if (ShopData.TruckUri != "Truck0.png") {
        $("#Truck").attr("src", BaseContentURL + ShopData.TruckUri);
        $("#Topper").attr("src", BaseContentURL + ShopData.TopperUri);
        $("#Menu").attr("src", BaseContentURL + ShopData.MenuUri);
        $("#Wheels").attr("src", BaseContentURL + ShopData.WheelsUri);
        $("#Sign").attr("src", BaseContentURL + ShopData.SignUri);
        $("#Trailer").attr("src", BaseContentURL + ShopData.TrailerUri);
        // If The Truck is showing, then show the inside and the Worker
        $("#TruckInside").attr("src", BaseContentURL + "TruckInside.png");
        // Worker
        // TODO:  Have the Worker be the Student's Avater...
        $("#Worker").attr("src", BaseContentAvatarURL + "AvatarHalf.png");
        // If the Truck is Showing, check to see if it is open for business or not
        // If not, hang the Close Sign
        if (ShopData.IsClosed) {
            $("#TruckClosedSign").attr("src", BaseContentURL + "ClosedSign.png");
            $("#Worker").attr("src", BaseContentAvatarURL + EmptyItem);
        }
    }
    else {
        // If the Truck is Empty, then just draw the default empty state
        DrawEmptyTruckItems();
    }
}
function DrawEmptyTruckItems() {
    $("#Truck").attr("src", BaseContentURL + EmptyItem);
    $("#Topper").attr("src", BaseContentURL + EmptyItem);
    $("#Menu").attr("src", BaseContentURL + EmptyItem);
    $("#Wheels").attr("src", BaseContentURL + EmptyItem);
    $("#Sign").attr("src", BaseContentURL + EmptyItem);
    $("#Trailer").attr("src", BaseContentURL + EmptyItem);
    $("#TruckInside").attr("src", BaseContentURL + EmptyItem);
    $("#TruckClosedSign").attr("src", BaseContentURL + EmptyItem);
    // $("#Backgrond").attr("src", BaseContentURL + EmptyItem); //"Background1.png");
    $("#Backgrond").attr("src", BaseContentURL + "Background1.png");
    $("#Worker").attr("src", BaseContentAvatarURL + EmptyItem);
}
/// The Transaction List is a List of strings of the latest transactions.
function ShowTransactionList() {
    var el = $('#TransactionList');
    el.empty();
    var max = ShopData.TransactionList.length - 10;
    if (max < 0) {
        max = 0;
    }
    for (var i = ShopData.TransactionList.length - 1; i > max; i--) {
        var item = ShopData.TransactionList[i];
        el.append(" &nbsp; <span>" + item.Name + "</span> &nbsp;");
        if (item.Uri != null) {
            el.append("<img class=\"ProductBoughtBar\" id = \"ProductBought\" src = \"/Content/shop/" + item.Uri + "\" alt = \"ProductBought\" /> &nbsp;");
        }
    }
    // Add the Latest Transactions to display
    //for (var item of ShopData.TransactionList)
    //{
    //    el.append(" &nbsp; <span>" + item.Name + "</span> &nbsp;");
    //    if (item.Uri != null) {
    //        el.append("<img class=\"ProductBoughtBar\" id = \"ProductBought\" src = \"/Content/shop/" + item.Uri + "\" alt = \"ProductBought\" /> &nbsp;");
    //    }
    //}
}
//# sourceMappingURL=game.js.map

(function () {

    //var ele = $("#username");
    //ele.text("PH X");
    
    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "background-color: #888;";
    //});
    //main.on("mouseleave", function () {
    //    main.style = "";
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    alert("Helloo");
    //});

    var $sideAndWrapper = $("#sidebar,#wrapper");

    $("#sidebarToggle").on("click", function () {
        $sideAndWrapper.toggleClass("hide-sidebar");
    });
     

})();
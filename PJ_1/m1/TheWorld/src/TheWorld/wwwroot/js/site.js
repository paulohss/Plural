
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
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {

        $sideAndWrapper.toggleClass("hide-sidebar"); //add class if it not already there, and remove it it is there

        if ($sideAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }else{
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        }
        });     

})();
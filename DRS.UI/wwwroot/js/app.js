$(document).ready(function () {
    $('.navbar-toggle').on('click', function (event) {
        $(this).toggleClass('open');
        $('#navigation').slideToggle(400);
    });

    $(window).resize(function () {
        if ($(window).width() >= 992) {
            $("#navigation").attr("display", "block");
        }
        else {
            $("#navigation").attr("display", "none");
        }
    })
    

    $('.navigation-menu>li').slice(-1).addClass('last-elements');

    $('.navigation-menu li.has-submenu a[href="#"]').on('click', function (e) {
        if ($(window).width() <= 993) {
            debugger;
            e.preventDefault();
            $(this).parent('li').toggleClass('open').find('.submenu:first').toggleClass('open');
        }
    });

    $(".has-submenu").click(function () {
        $(this).children("ul").toggle();
    });

    $(".navigation-menu a").each(function () {
        
        if (this.href == window.location.href) {
            $(this).parent().addClass("active"); // add active to li of the current link
            $(this).parent().parent().parent().addClass("active"); // add active class to an anchor
            $(this).parent().parent().parent().parent().parent().addClass("active"); // add active class to an anchor
        }
    });

    //$('.slimscroll-noti').slimScroll({
    //    height: '230px',
    //    position: 'right',
    //    size: "5px",
    //    color: '#98a6ad',
    //    wheelStep: 10
    //});
});

function bindFormSm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModalLg').modal('hide');
                    $('#replacetarget').load(result.url); //  Load data from the server and place the returned HTML into the matched element
                } else {
                    $('#myModalContentLg').html(result);
                    bindFormLg(dialog);
                }
            }
        });
        return false;
    });

}

//Funciones de los modales
function bindFormLg(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModalLg').modal('hide');
                    $('#replacetarget').load(result.url); //  Load data from the server and place the returned HTML into the matched element
                } else {
                    $('#myModalContentLg').html(result);
                    bindFormLg(dialog);
                }
            }
        });
        return false;
    });

}

function bindFormXl(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModalXl').modal('hide');
                    $('#replacetarget').load(result.url); //  Load data from the server and place the returned HTML into the matched element
                } else {
                    $('#myModalContentXl').html(result);
                    bindFormLg(dialog);
                }
            }
        });
        return false;
    });

}

function bindFormMd(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModalMd').modal('hide');
                    $('#replacetarget').load(result.url);
                }
                else {
                    $('#myModalContentMd').html(result);
                    bindFormMd(dialog);
                }
            }
        });
        return false;
    });
}

////!function ($) {
////    "use strict";

////    var MainApp = function () { };

////    MainApp.prototype.initNavbar = function () {

////        $('.navbar-toggle').on('click', function (event) {
////            $(this).toggleClass('open');
////            $('#navigation').slideToggle(400);
////        });

////        $('.navigation-menu>li').slice(-1).addClass('last-elements');

////        $('.navigation-menu li.has-submenu a[href="#"]').on('click', function (e) {
////            if ($(window).width() < 992) {
////                e.preventDefault();
////                $(this).parent('li').toggleClass('open').find('.submenu:first').toggleClass('open');
////            }
////        });
////    },
////        MainApp.prototype.initLoader = function () {
////            // Preloader
////            $(window).on('load', function () {
////                $('#status').fadeOut();
////                $('#preloader').delay(350).fadeOut('slow');
////            });
////        },
////        MainApp.prototype.initScrollbar = function () {
////            $('.slimscroll-noti').slimScroll({
////                height: '230px',
////                position: 'right',
////                size: "5px",
////                color: '#98a6ad',
////                wheelStep: 10
////            });
////        }
////    // === fo,llowing js will activate the menu in left side bar based on url ====
////    MainApp.prototype.initMenuItem = function () {
////        $(".navigation-menu a").each(function () {
////            if (this.href == window.location.href) {
////                $(this).parent().addClass("active"); // add active to li of the current link
////                $(this).parent().parent().parent().addClass("active"); // add active class to an anchor
////                $(this).parent().parent().parent().parent().parent().addClass("active"); // add active class to an anchor
////            }
////        });
////    },
////        MainApp.prototype.initComponents = function () {
////            $('[data-toggle="tooltip"]').tooltip();
////            $('[data-toggle="popover"]').popover();
////        },
////        MainApp.prototype.initToggleSearch = function () {
////            $('.toggle-search').on('click', function () {
////                var targetId = $(this).data('target');
////                var $searchBar;
////                if (targetId) {
////                    $searchBar = $(targetId);
////                    $searchBar.toggleClass('open');
////                }
////            });
////        },

////        MainApp.prototype.init = function () {
////            this.initNavbar();
////            this.initLoader();
////            this.initScrollbar();
////            this.initMenuItem();
////            this.initComponents();
////            this.initToggleSearch();
////        },

////        //init
////        $.MainApp = new MainApp, $.MainApp.Constructor = MainApp
////}(window.jQuery),

////    //initializing
////    function ($) {
////        "use strict";
////        $.MainApp.init();
////    }(window.jQuery);
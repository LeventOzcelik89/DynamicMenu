
var page = {
    ready: function () {

    },
    showMenu: function (item) {

        var elem = $('.page[data-page="' + $(item).attr('data-page') + '"]');

        $('.page').addClass('hidden');
        $(elem).removeClass('hidden');
    },
    backMenu: function (item) {

        var elem = $('#' + $(item).attr('data-target'));
        page.showMenu(elem);
    }
}

$(document)

    .ready(function () {

        //var pages = $('.page');
        //var mainMenu = $('#main-menu');

        page.ready();

    })

    .on('click', '.menu-item', function (e) {

        e.preventDefault();

        page.showMenu(this);

        return false;

    })

    .on('click', '.back-button', function (e) {

        e.preventDefault();

        page.backMenu(this);

        return false;

    })

    ;

function showPage(item) {

}

//document.addEventListener('DOMContentLoaded', function () {
//    // Page navigation
//    const pages = document.querySelectorAll('.page');
//    const mainMenu = document.getElementById('main-menu');
//    function showPage(pageId) {

//        $('.page').addClass('hidden');

//        pages.forEach(page => {
//            if (page.id === pageId) {
//                page.classList.remove('hidden');
//                page.classList.add('page-enter');
//                //setTimeout(() => page.classList.add('page-enter-active'), 10);
//                //setTimeout(() => {
//                //    page.classList.remove('page-enter', 'page-enter-active');
//                //}, 300);
//            } else {
//                page.classList.add('hidden');
//            }
//        });
//    }
//    // Menu item click handler
//    const menuItems = document.querySelectorAll('.menu-item');
//    menuItems.forEach(item => {
//        item.addEventListener('click', function (e) {
//            // Add ripple effect
//            //const ripple = document.createElement('div');
//            //ripple.classList.add('ripple');
//            //const rect = this.getBoundingClientRect();
//            //const size = Math.max(rect.width, rect.height);
//            //ripple.style.width = ripple.style.height = `${size}px`;
//            //ripple.style.left = `${e.clientX - rect.left - size/2}px`;
//            //ripple.style.top = `${e.clientY - rect.top - size/2}px`;
//            //this.appendChild(ripple);
//            //setTimeout(() => {
//            //    ripple.remove();
//            //}, 300);
//            // Navigate to submenu page if available
//            const targetPage = this.dataset.page;
//            if (targetPage) {
//                showPage(targetPage);
//            }
//            // Handle menu item click
//            const menuText = this.querySelector('span').textContent;
//            console.log(`Selected menu: ${menuText}`);
//        });
//    });
//    // Back button handler
//    const backButtons = document.querySelectorAll('.back-button');
//    backButtons.forEach(button => {
//        button.addEventListener('click', function () {
//            const targetPage = this.dataset.target;
//            showPage(targetPage);
//        });
//    });
//    // Bottom navigation handler
//    const navItems = document.querySelectorAll('.nav-item');
//    navItems.forEach(item => {
//        item.addEventListener('click', function () {
//            navItems.forEach(nav => nav.classList.remove('active'));
//            this.classList.add('active');
//            // Always show main menu when switching tabs
//            showPage('main-menu');
//        });
//    });
//    // Add touch feedback
//    document.querySelectorAll('.menu-item, .nav-item').forEach(item => {
//        item.addEventListener('touchstart', function () {
//            this.style.backgroundColor = '#f0f0f0';
//        });
//        item.addEventListener('touchend', function () {
//            this.style.backgroundColor = '';
//        });
//    });
//});
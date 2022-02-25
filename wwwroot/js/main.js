//Global
var GLOBAL = {};
GLOBAL.HeaderDotNetReference = null;
GLOBAL.MainLayouteDotNetReference = null;
GLOBAL.PageDotNetReference = null;
GLOBAL.FooterDotNetReference = null;
function SetHeaderDotnetReference(headerDotNetReference) { GLOBAL.HeaderDotNetReference = headerDotNetReference; };
function SetPageDotnetReference(pageDotNetReference) { GLOBAL.PageDotNetReference = pageDotNetReference; };
function SetMainLayouteDotnetReference(mainLayoutDotNetReference) { GLOBAL.MainLayouteDotNetReference = mainLayoutDotNetReference; };
function SetFooterDotnetReference(footerDotNetReference) { GLOBAL.FooterDotNetReference = footerDotNetReference; };

//Document
function getCurrentWindowInnerWidth() { return window.innerWidth; }
function getCurrentScrollTop() { return document.documentElement.scrollTop; }
$(window).scroll((e) => {
    if ($('.subcategories').hasClass('show')) {
        GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSubcategoriesAsync');
    }
    if ($('.panel__search__lupa').hasClass("active") && $('.panel__search__input').hasClass("active")) {
        GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSearchPanel');
    }
    if (getCurrentScrollTop() >= 835) {
        GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('ShowScrollToUp');
    }
    else {
        GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('HideScrollToUp');
    }
    let st = window.pageYOffset || document.documentElement.scrollTop;
    let lastScrollTop = 0;
    if (st > lastScrollTop) {
        if (document.documentElement.scrollTop > 0) {
            GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideUserPanelAsync');
            GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSubcategoriesAsync');
            GLOBAL.HeaderDotNetReference.invokeMethodAsync('EnableHeaderScrolling');
            GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('EnableCategorySideNavigationIsScroling');
            GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('EnableReturnButtonScroling');
        }
    } else {
        if (document.documentElement.scrollTop == 0) {
            GLOBAL.HeaderDotNetReference.invokeMethodAsync('СancelHeaderScrolling');
            GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('СancelCategorySideNavigationIsScroling');
            GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('СancelReturnButtonScroling');
        }
    }
    lastScrollTop = st <= 0 ? 0 : st;
})
$(document).click((e) => {
    if (!e.target.closest('.subcategories, .category__link')) {
        if ($('.subcategories').hasClass('show')) {
            GLOBAL.HeaderDotNetReference.invokeMethodAsync("HideSubcategoriesAsync");
        }
    }
    if (!e.target.closest('.panel__search__lupa, .panel__search__input')) {
        if ($(".panel__search__lupa").hasClass("active") && $(".panel__search__input").hasClass("active")) {
            GLOBAL.HeaderDotNetReference.invokeMethodAsync('HideSearchPanel');
        }
    }
})
$(window).resize((e) => {
    if ($('.subcategories').hasClass('show')) {
        GLOBAL.DotNetReference.invokeMethodAsync("HideSubcategoriesAsync");
    }
    if ($(".panel__search__lupa").hasClass("active") && $(".panel__search__input").hasClass("active")) {
        GLOBAL.DotNetReference.invokeMethodAsync('HideSearchPanel');
    }
})

//MainLayout
function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    })
}
function openCategorySideNavigation(classWidthCategorySideNavagation) {
    GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('OpenCategorySideNavigation', classWidthCategorySideNavagation);
}
function openSubcategorySideNavigation(classWidthSubcategorySideNavagation) {
    GLOBAL.MainLayouteDotNetReference.invokeMethodAsync('OpenSubcategorySideNavigation', classWidthSubcategorySideNavagation);
}

//Header
function showSubcategories() {
    if (!$('.subcategories').hasClass('show')) {
        $('.subcategories').slideDown(200);
        $('.subcategories').css('display', 'flex');
    }
}
function hideSubcategories() {
    if ($('.subcategories').hasClass('show')) {
        $('.subcategories').slideUp(200);
    }
}
function showUserPanel() {
    if (!$('.panel__user-links').hasClass('show')) {
        $('.panel__user-links').slideDown(150);
    }
}
function hideUserPanel() {
    if ($('.panel__user-links').hasClass('show')) {
        $('.panel__user-links').slideUp(150);
    }
}

//Footer
function showSpoilerContent(spoilerContent) {
    $('.spoiler__content').not($(this)).slideUp(300);
    if ($(spoilerContent).hasClass('active')) {
        hideSpoilerContent(spoilerContent);
    } else {
        $(spoilerContent).slideDown(300);
    }
}
function hideSpoilerContent(spoilerContent) {
    console.log('Зачистка')
    $('.spoiler__content').not($(this)).slideUp(300);
    if (!$(spoilerContent).hasClass('active')) {
        $(spoilerContent).slideUp(300);
    } else {
        showSpoilerContent(spoilerContent);
    }
}

//Register
function getScorePassword(password) {
    var score = 0;
    if (!password)
        return score;

    // award every unique letter until 5 repetitions
    var letters = new Object();
    for (var i = 0; i < password.length; i++) {
        letters[password[i]] = (letters[password[i]] || 0) + 1;
        score += 5.0 / letters[password[i]];
    }

    // bonus points for mixing it up
    var variations = {
        digits: /\d/.test(password),
        lower: /[a-z]/.test(password),
        upper: /[A-Z]/.test(password),
        nonWords: /\W/.test(password),
    }

    var variationCount = 0;
    for (var check in variations) {
        variationCount += (variations[check] == true) ? 1 : 0;
    }
    score += (variationCount - 1) * 10;

    return parseInt(score);
}

$('.counter__plus').click(e => {
    const counterInput = $(e.target).prev()[0];
    counterInput.value++;
    if(counterInput.value > 1) $($(e.target).parent().next()[0]).slideDown(100);
    sum = (27999 * counterInput.value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    sumDiscount = (Math.round((27999 * counterInput.value) * 0.9)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    $($($($(e.target).parent()).parent()).parent().find('.block-price__price')  [0]).html(sum + ' ₽')
    $($($($(e.target).parent()).parent()).parent().find('.block-price__discount-price')[0]).html(sumDiscount + ' ₽')
})
$('.counter__minus').click(e => {
    const counterInput = $(e.target).next()[0]
    if(counterInput.value > 1) counterInput.value--;
    if(counterInput.value == 1) $($(e.target).parent().next()[0]).css('transform', 'translateY(-10px)').slideUp(100);
    sum = (27999 * counterInput.value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    sumDiscount = (Math.round((27999 * counterInput.value) * 0.9)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    $($($($(e.target).parent()).parent()).parent().find('.block-price__price')[0]).html(sum + ' ₽')
    $($($($(e.target).parent()).parent()).parent().find('.block-price__discount-price')[0]).html(sumDiscount + ' ₽')
})
$('.counter-input').on('input', function () {
    $(this).val($(this).val().replace(/[A-Za-zА-Яа-яЁё]/, ''))
});

$('.counter-input').on('input', e => {
    e.target.value = e.target.value.replace(/[^0-9]/g, '');
    if(e.target.value <= 1) {
        $($(e.target).parent().next()[0]).css('transform', 'translateY(-10px)').slideUp(100);
        e.target.value = 1;
    }
    if(e.target.value > 1) $($(e.target).parent().next()[0]).slideDown(100); 
    sum = (27999 * e.target.value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    sumDiscount = (Math.round((27999 * counterInput.value) * 0.9)).toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    $($($($(e.target).parent()).parent()).parent().find('.block-price__price')[0]).html(sum + ' ₽')
    $($($($(e.target).parent()).parent()).parent().find('.block-price__discount-price')[0]).html(sumDiscount + ' ₽')
})
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

    let element = $(e.target);
    let filterArrow, filterSpoiler;
    if (element.hasClass('label-block')) {
        filterArrow = $(element.children('.label-block__arrow'));
        filterSpoiler = $(element.next('.filter-spoiler'));

        if (filterArrow.hasClass('show')) {
            filterArrow.removeClass('show');
            filterSpoiler.slideUp(150);
        } else {
            filterArrow.addClass('show');
            filterSpoiler.slideDown(150);
        }
    } else if (element.hasClass('label-block__arrow') || element.hasClass('filter-label')) {
        var labelBlock = $(element).parent();
        filterArrow = $(labelBlock.children('.label-block__arrow'));
        filterSpoiler = $(labelBlock.next('.filter-spoiler'));

        if (filterArrow.hasClass('show')) {
            filterArrow.removeClass('show');
            filterSpoiler.slideUp(150);
        } else {
            filterArrow.addClass('show');
            filterSpoiler.slideDown(150);
        }
    }
})
$(window).resize((e) => {
    if ($('.subcategories').hasClass('show')) {
        GLOBAL.DotNetReference.invokeMethodAsync("HideSubcategoriesAsync");
    }
    if ($(".panel__search__lupa").hasClass("active") && $(".panel__search__input").hasClass("active")) {
        GLOBAL.DotNetReference.invokeMethodAsync('HideSearchPanel');
        closeZoomedMainImage()
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

//Product-Card
function openZoomedMainImage() {
    let imageBlock = $(".images-block__main-image");
    let whiteReact = $('.main-image__white-rect');
    let zoomedImage = $('.main-image__zoomed-main-image');
    let mainImage = $(".main-image__image");
    zoomedImage.css('background-size', `${mainImage.width() * 3}px ${mainImage.height() * 3}px`);
    $(".main-image__image").on('mousemove', image => {
        let imageX = image.pageX - imageBlock[0].offsetLeft;
        let imageY = image.pageY - imageBlock[0].offsetTop;
        if (imageX + whiteReact[0].offsetWidth / 2 > imageBlock[0].offsetWidth) {
            imageX = imageBlock[0].offsetWidth - whiteReact[0].offsetWidth / 2;
        }
        if (imageY + whiteReact[0].offsetHeight / 2 > imageBlock[0].offsetHeight) {
            imageY = imageBlock[0].offsetHeight - whiteReact[0].offsetHeight / 2;
        }
        if (imageX - whiteReact[0].offsetWidth / 2 < 0) {
            imageX = whiteReact[0].offsetWidth / 2;
        }
        if (imageY - whiteReact[0].offsetHeight / 2 < 0) {
            imageY = whiteReact[0].offsetHeight / 2;
        }
        whiteReact.css('left', imageX - whiteReact[0].offsetWidth / 2);
        whiteReact.css('top', imageY - whiteReact[0].offsetHeight / 2);
        imageX = image.pageX - imageBlock[0].offsetLeft - whiteReact[0].offsetWidth / 2;
        imageY = image.pageY - imageBlock[0].offsetTop - whiteReact[0].offsetHeight / 2;
        if (imageX + whiteReact[0].offsetWidth > image.target.offsetWidth) {
            imageX = imageBlock[0].offsetWidth - whiteReact[0].offsetWidth;
        }
        if (imageY + whiteReact[0].offsetHeight > imageBlock[0].offsetHeight) {
            imageY = imageBlock[0].offsetHeight - whiteReact[0].offsetHeight;
        }
        if (imageX < 0) {
            imageX = 0;
        }
        if (imageY < 0) {
            imageY = 0;
        }
        zoomedImage.css('background-position', `-${imageX * 3}px -${imageY * 3}px`);
    })
    whiteReact.css('display', 'block');
    zoomedImage.css('display', 'block');
}
function closeZoomedMainImage() {
    $('.main-image__white-rect').css('display', 'none');
    $('.main-image__zoomed-main-image').css('display', 'none');
}

//Product-Catalog
function openFilters() {
    $('.sort-by-block__choose-block').slideDown(150);
}
function closeFilters() {
    $('.sort-by-block__choose-block').slideUp(150);
}
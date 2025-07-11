function toggleTheme(isLight) {
    localStorage.setItem('sorcery-theme', isLight ? 'light' : 'dark');
    
    const disableOnLight = document.getElementsByClassName("sorcery-disabled-on-light");
    const disableOnDark = document.getElementsByClassName("sorcery-disabled-on-dark");
    const hideOnLight = document.getElementsByClassName("sorcery-hidden-on-light");
    const hideOnDark = document.getElementsByClassName("sorcery-hidden-on-dark");

    const toDisable = isLight ? disableOnLight : disableOnDark;
    const toEnable = isLight ? disableOnDark : disableOnLight;
    const toHide = isLight ? hideOnLight : hideOnDark;
    const toShow = isLight ? hideOnDark : hideOnLight;

    for (const d of toDisable) {
        d.setAttribute('disabled', '');
    }
    for (const e of toEnable) {
        e.removeAttribute('disabled');
    }
    for (const h of toHide) {
        h.setAttribute('hidden', '');
    }
    for (const s of toShow) {
        s.removeAttribute('hidden');
    }
}

// Switch to light mode.
function enableLightMode() {
    toggleTheme(true);
}

// Switch to light mode.
function enableDarkMode() {
    toggleTheme(false);
}

document.addEventListener("DOMContentLoaded", (event) => {
    const lightModeButton = document.getElementById('enable-light-mode-button');
    const darkModeButton = document.getElementById('enable-dark-mode-button');
    lightModeButton.addEventListener('click', enableLightMode);
    darkModeButton.addEventListener('click', enableDarkMode);
});

if (localStorage.getItem('sorcery-theme') === 'light') {
    enableLightMode();
} else {
    enableDarkMode();
}
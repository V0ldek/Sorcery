function reloadGiscus(isLight) {
    const giscusPaperElement = document.getElementById('comment-box-paper');
    const giscusScriptElement = document.getElementById('sorcery-giscus-script');

    if (giscusScriptElement) {
        const theme = isLight
            ? 'https://v0ldek.com/css/bundled/giscus-light.min.css'
            : 'https://v0ldek.com/css/bundled/giscus.min.css';
        giscusScriptElement.setAttribute('data-theme', theme);
        giscusScriptElement.setAttribute('src', `https://giscus.app/client.js?cachebuster=${isLight}`);
        const newElement = document.createElement('script');
        [...giscusScriptElement.attributes].forEach(attr => {
            newElement.setAttribute(attr.nodeName, attr.nodeValue)
        })
        for (const child of giscusPaperElement.children) {
            giscusPaperElement.removeChild(child);
        }
        giscusPaperElement.appendChild(newElement);
    }
}

function repaintTheme(isLight) {
    const disableOnLight = document.getElementsByClassName("sorcery-disabled-on-light");
    const disableOnDark = document.getElementsByClassName("sorcery-disabled-on-dark");
    const basicHideOnLight = document.getElementsByClassName("sorcery-hidden-on-light");
    const basicHideOnDark = document.getElementsByClassName("sorcery-hidden-on-dark");

    // Prism wraps code blocks in an additional div and then adds things like buttons under that div.
    // When switching themes we need to hide the entire parent div.
    const prismHideOnLight = document.querySelectorAll(
        '.code-toolbar:has(.sorcery-code-block.sorcery-hidden-on-light)');
    const prismHideOnDark = document.querySelectorAll(
        '.code-toolbar:has(.sorcery-code-block.sorcery-hidden-on-dark)');
    const hideOnLight = [...basicHideOnLight, ...prismHideOnLight];
    const hideOnDark = [...basicHideOnDark, ...prismHideOnDark];

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

    reloadGiscus(isLight);
}

// Switch to light mode.
function enableLightMode() {
    localStorage.setItem('sorcery-theme', 'light');
    repaintTheme(true);
}

// Switch to light mode.
function enableDarkMode() {
    localStorage.setItem('sorcery-theme', 'dark');
    repaintTheme(false);
}

function applyStoredTheme() {
    if (localStorage.getItem('sorcery-theme') === 'light') {
        repaintTheme(true);
    } else {
        repaintTheme(false);
    }
}

document.addEventListener("DOMContentLoaded", (event) => {
    const lightModeButton = document.getElementById('enable-light-mode-button');
    const darkModeButton = document.getElementById('enable-dark-mode-button');
    lightModeButton.addEventListener('click', enableLightMode);
    darkModeButton.addEventListener('click', enableDarkMode);
    applyStoredTheme();
});

applyStoredTheme();
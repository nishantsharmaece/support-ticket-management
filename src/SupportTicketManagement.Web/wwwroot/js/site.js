(function () {
    const storageKey = 'stm-theme';
    const root = document.documentElement;

    function getStoredTheme() {
        return localStorage.getItem(storageKey) || 'light';
    }

    function applyTheme(theme) {
        root.setAttribute('data-theme', theme);
        localStorage.setItem(storageKey, theme);
        updateThemeToggle(theme);
    }

    function updateThemeToggle(theme) {
        const toggle = document.getElementById('theme-toggle');
        if (!toggle) {
            return;
        }

        const isDark = theme === 'dark';
        toggle.checked = isDark;
        toggle.setAttribute('aria-label', isDark ? 'Switch to light theme' : 'Switch to dark theme');
    }

    document.addEventListener('DOMContentLoaded', function () {
        const toggle = document.getElementById('theme-toggle');
        if (toggle) {
            toggle.addEventListener('change', function () {
                applyTheme(toggle.checked ? 'dark' : 'light');
            });
        }

        updateThemeToggle(getStoredTheme());
    });
})();

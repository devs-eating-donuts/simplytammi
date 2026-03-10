window.simplyTammiContact = {
    _token: "",
    _widgetId: null,

    renderTurnstile: function (siteKey) {
        var self = window.simplyTammiContact;
        self._token = "";

        var container = document.getElementById("turnstile-container");
        if (!container) return;

        container.innerHTML = "";

        if (!window.turnstile) {
            console.warn("Turnstile API not loaded yet. Retrying in 500ms...");
            setTimeout(function () { self.renderTurnstile(siteKey); }, 500);
            return;
        }

        self._widgetId = window.turnstile.render(container, {
            sitekey: siteKey,
            callback: function (token) { self._token = token ?? ""; },
            "expired-callback": function () { self._token = ""; },
            "error-callback": function () { self._token = ""; }
        });
    },

    getTurnstileToken: function () {
        var self = window.simplyTammiContact;

        if (self._token) {
            return self._token;
        }

        if (window.turnstile && self._widgetId != null) {
            var token = window.turnstile.getResponse(self._widgetId);
            if (token) return token;
        }

        return "";
    }
};

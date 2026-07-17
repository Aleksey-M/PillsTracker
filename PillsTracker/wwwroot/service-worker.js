// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });


self.addEventListener('install', event => {
    console.log('[Service Worker] Installed');
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    console.log('[Service Worker] Activated');
    return self.clients.claim();
});

// Получение сообщений от Blazor (UI)
self.addEventListener('message', event => {
    if (event.data && event.data.type === 'schedule-notification') {
        scheduleNotification(event.data.payload);
    }
});

// Планирование уведомления
function scheduleNotification({ title, body, timestamp }) {
    const delay = timestamp - Date.now();

    if (delay <= 0) {
        showNotification(title, body);
        return;
    }

    // setTimeout работает в сервис воркере, пока он активен
    setTimeout(() => {
        showNotification(title, body);
    }, delay);
}

// Показ уведомления
function showNotification(title, body) {
    self.registration.showNotification(title, {
        body: body,
        icon: '/icons/icon-192.png',
        badge: '/icons/badge-72.png'
    });
}

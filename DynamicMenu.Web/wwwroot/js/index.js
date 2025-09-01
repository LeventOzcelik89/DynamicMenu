document.addEventListener('DOMContentLoaded', function () {
    // Menü şablonları için veri yapıları
    const menuTemplates = {
        'menu-personel': {
            title: 'Personel Menüsü',
            items: [
                { id: 'personel-bilgileri', title: 'Personel Bilgileri', icon: 'fas fa-user' },
                { id: 'izin-islemleri', title: 'İzin İşlemleri', icon: 'fas fa-calendar-alt' },
                { id: 'bordro', title: 'Bordro Görüntüleme', icon: 'fas fa-file-invoice-dollar' },
                { id: 'mesai', title: 'Mesai Bilgileri', icon: 'fas fa-business-time' },
                { id: 'egitimler', title: 'Eğitimler', icon: 'fas fa-graduation-cap' }
            ]
        },
        'menu-cocuk': {
            title: 'Çocuk Menüsü',
            items: [
                { id: 'oyunlar', title: 'Oyunlar', icon: 'fas fa-gamepad' },
                { id: 'aktiviteler', title: 'Aktiviteler', icon: 'fas fa-running' },
                { id: 'kumbaralar', title: 'Kumbaralar', icon: 'fas fa-piggy-bank' },
                { id: 'videolar', title: 'Eğitici Videolar', icon: 'fas fa-film' }
            ]
        },
        'menu-mobile': {
            title: 'Mobil Menü',
            items: [
                { id: 'para-transferleri', title: 'Para Transferleri', icon: 'fas fa-exchange-alt' },
                { id: 'odemeler', title: 'Ödemeler', icon: 'fas fa-money-bill-wave' },
                { id: 'kart-islemleri', title: 'Kart İşlemleri', icon: 'fas fa-credit-card' },
                { id: 'yatirim', title: 'Yatırım ve Hisse Senedi', icon: 'fas fa-chart-line' },
                { id: 'doviz', title: 'Döviz ve Altın', icon: 'fas fa-coins' }
            ]
        }
    };

    // Alt menüler için örnekler
    const subMenuTemplates = {
        'para-transferleri': {
            title: 'Para Transferleri',
            items: [
                { id: 'kayitli-transferler', title: 'Kayıtlı Transferler', icon: 'fas fa-bookmark' },
                { id: 'hesaplar-arasi-transfer', title: 'Hesaplarım Arası Transfer', icon: 'fas fa-exchange-alt' },
                { id: 'transfer-emirleri', title: 'Transfer Emirleri', icon: 'fas fa-tasks' },
                { id: 'kolay-adres', title: 'Kolay Adres', icon: 'fas fa-map-marker-alt' },
                { id: 'odeme-iste', title: 'Ödeme İste', icon: 'fas fa-clock' }
            ]
        },
        'odemeler': {
            title: 'Ödemeler',
            items: [
                { id: 'fatura-odeme-talimatlari', title: 'Fatura Ödeme Talimatlarım', icon: 'fas fa-file-invoice' },
                { id: 'kayitli-odemeler', title: 'Kayıtlı Ödemeler', icon: 'fas fa-history' },
                { id: 'fatura-kurum-odemesi', title: 'Fatura ve Kurum Ödemesi', icon: 'fas fa-building' }
            ]
        }
    };

    // Geçerli olarak seçilen menü öğesi ve menü ID'si
    let selectedMenuItem = null;
    let currentMenuId = null;

    // DataTables ayarlarını yapılandırma
    const menuTable = $('#menu-table').DataTable({
        responsive: true,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.11.5/i18n/tr.json'
        },
        columnDefs: [
            { targets: -1, orderable: false }
        ]
    });

    // Menü önizlemesini yükle
    function loadMenuPreview(menuId) {
        const menuPreview = document.getElementById('menu-preview');
        const menuTemplate = document.getElementById(`template-${menuId}`);

        if (menuTemplate && menuPreview) {
            // Menü şablonunun bir kopyasını oluştur
            const menuContent = menuTemplate.cloneNode(true);
            menuContent.classList.remove('hidden');
            menuContent.removeAttribute('id');

            // Önizleme alanını temizle ve yeni içeriği ekle
            menuPreview.innerHTML = '';
            menuPreview.appendChild(menuContent);

            // Menü başlığını güncelle
            const editorTitle = document.getElementById('menu-editor-title');
            if (editorTitle && menuTemplates[menuId]) {
                editorTitle.textContent = `${menuTemplates[menuId].title} Düzenleme`;
            }

            // Menü öğelerine tıklamalar için olay dinleyiciler ekle
            menuPreview.querySelectorAll('.menu-item').forEach(item => {
                item.addEventListener('click', handleMenuItemClick);
            });

            // Geçerli menü ID'sini kaydet
            currentMenuId = menuId;
        } else {
            console.error('Menü şablonu veya önizleme alanı bulunamadı');
        }
    }

    // Menü öğesine tıklama olayını işle
    function handleMenuItemClick(e) {
        // Daha önce seçilen öğeyi temizle
        if (selectedMenuItem) {
            selectedMenuItem.classList.remove('selected');
        }

        // Yeni tıklanan öğeyi seç
        this.classList.add('selected');
        selectedMenuItem = this;

        // Menü öğesi ID'sini kaydet
        const menuItemId = this.dataset.menuId;
        console.log(`Seçilen menü öğesi: ${menuItemId}`);

        // Alt menü varsa yükleme işlemleri burada yapılabilir
        // Şimdilik sadece seçilen öğeyi logla
        if (subMenuTemplates[menuItemId]) {
            console.log(`Bu öğe (${menuItemId}) için bir alt menü mevcut`);
        }
    }

    // DataTable içindeki düzenleme butonlarına tıklama olayları
    document.querySelectorAll('.edit-button').forEach(button => {
        button.addEventListener('click', function () {
            const targetPage = this.dataset.target;
            const menuId = this.dataset.menuId;

            if (targetPage && menuId) {
                // Menü düzenleme sayfasına geç
                showPage(targetPage);

                // Seçilen menüyü önizlemeye yükle
                loadMenuPreview(menuId);
            }
        });
    });

    // Geri butonu için olay dinleyici
    document.querySelectorAll('.back-button').forEach(button => {
        button.addEventListener('click', function () {
            const targetPage = this.dataset.target;
            if (targetPage) {
                showPage(targetPage);

                // Seçilen öğeyi temizle
                if (selectedMenuItem) {
                    selectedMenuItem.classList.remove('selected');
                    selectedMenuItem = null;
                }
            }
        });
    });

    // Menü işlemleri butonları
    const newMenuBtn = document.getElementById('new-menu');
    const deleteSelectedMenuBtn = document.getElementById('delete-selected-menu');
    const addMenuItemBtn = document.getElementById('add-menu-item');
    const createSubmenuBtn = document.getElementById('create-submenu');
    const deleteMenuItemBtn = document.getElementById('delete-menu-item');

    // Yeni menü ekle butonu
    if (newMenuBtn) {
        newMenuBtn.addEventListener('click', function () {
            console.log('Yeni menü oluştur tıklandı');
            alert('Yeni menü oluşturma özelliği hazırlanıyor...');
        });
    }

    // Tablo sil butonları
    document.querySelectorAll('.delete-button').forEach(button => {
        button.addEventListener('click', function () {
            const menuId = this.dataset.menuId;
            const menuName = menuTemplates[menuId]?.title || menuId;
            console.log(`Menüyü sil: ${menuName}`);

            if (confirm(`"${menuName}" menüsünü silmek istediğinize emin misiniz?`)) {
                alert(`"${menuName}" menüsü silindi.`);
                // Gerçek uygulamada burada silme işlemi yapılır
                // Örneğin: menuTable.row($(this).parents('tr')).remove().draw();
            }
        });
    });

    // Tablo kopya butonları
    document.querySelectorAll('.copy-button').forEach(button => {
        button.addEventListener('click', function () {
            const menuId = this.dataset.menuId;
            const menuName = menuTemplates[menuId]?.title || menuId;
            console.log(`Menüyü kopyala: ${menuName}`);

            alert(`"${menuName}" menüsünün bir kopyası oluşturuldu.`);
            // Gerçek uygulamada burada kopyalama işlemi yapılır
        });
    });

    // Menü öğesi ekle butonu
    if (addMenuItemBtn) {
        addMenuItemBtn.addEventListener('click', function () {
            if (currentMenuId) {
                console.log(`"${currentMenuId}" menüsüne yeni öğe ekle`);
                alert('Menü öğesi ekleme özelliği hazırlanıyor...');
            } else {
                alert('Bir hata oluştu, lütfen tekrar deneyin');
            }
        });
    }

    // Alt menü oluştur butonu
    if (createSubmenuBtn) {
        createSubmenuBtn.addEventListener('click', function () {
            if (selectedMenuItem) {
                const menuItemId = selectedMenuItem.dataset.menuId;
                console.log(`"${menuItemId}" için alt menü oluştur`);
                alert(`"${menuItemId}" için alt menü oluşturma özelliği hazırlanıyor...`);
            } else {
                alert('Lütfen önce bir menü öğesi seçin');
            }
        });
    }

    // Menü öğesini sil butonu
    if (deleteMenuItemBtn) {
        deleteMenuItemBtn.addEventListener('click', function () {
            if (selectedMenuItem) {
                const menuItemId = selectedMenuItem.dataset.menuId;
                console.log(`"${menuItemId}" menü öğesini sil`);
                alert(`"${menuItemId}" menü öğesini silme özelliği hazırlanıyor...`);
            } else {
                alert('Lütfen önce bir menü öğesi seçin');
            }
        });
    }

    // DataTable satır seçim olayları
    $('#menu-table tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            menuTable.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    // Sayfa yüklendiğinde menü listesi sayfasını göster
    showPage('menu-list');
});
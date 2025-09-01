document.addEventListener('DOMContentLoaded', function () {
    // Rehber elemanları
    const tutorialOverlay = document.getElementById('tutorial-overlay');
    const tutorialCloseBtn = document.querySelector('.tutorial-close-btn');
    const tutorialPrevBtn = document.getElementById('tutorial-prev-btn');
    const tutorialNextBtn = document.getElementById('tutorial-next-btn');
    const tutorialSteps = document.querySelectorAll('.tutorial-step');
    const tutorialIndicators = document.querySelectorAll('.tutorial-indicator');
    const helpButton = document.getElementById('help-button');

    // Geçerli adım
    let currentStep = 1;
    const totalSteps = tutorialSteps.length;

    // LocalStorage'dan rehber gösterim durumunu kontrol et
    const hasSeenTutorial = localStorage.getItem('hasSeenTutorial');

    // İlk kez giriş yapan kullanıcıya rehberi göster
    if (!hasSeenTutorial) {
        showTutorial();
    }

    // Yardım butonuna tıklanınca rehberi göster
    if (helpButton) {
        helpButton.addEventListener('click', showTutorial);
    }

    // Rehberi göster
    function showTutorial() {
        tutorialOverlay.classList.add('active');
        updateTutorialView();
    }

    // Rehberi kapat
    function closeTutorial() {
        tutorialOverlay.classList.remove('active');

        // Kullanıcının rehberi gördüğünü kaydet
        localStorage.setItem('hasSeenTutorial', 'true');

        // İlk adıma geri dön (rehber tekrar açılırsa)
        currentStep = 1;
        updateTutorialView();
    }

    // Rehber görünümünü güncelle
    function updateTutorialView() {
        // Tüm adımları gizle ve aktif olanı göster
        tutorialSteps.forEach(step => {
            step.classList.remove('active');
        });
        document.querySelector(`.tutorial-step[data-step="${currentStep}"]`).classList.add('active');

        // Gösterge noktalarını güncelle
        tutorialIndicators.forEach(indicator => {
            indicator.classList.remove('active');
        });
        document.querySelector(`.tutorial-indicator[data-step="${currentStep}"]`).classList.add('active');

        // İleri/geri butonlarını güncelle
        tutorialPrevBtn.disabled = currentStep === 1;

        // Son adımdaysak İleri butonunun yazısını "Başla" olarak değiştir
        if (currentStep === totalSteps) {
            tutorialNextBtn.textContent = 'Başla';
        } else {
            tutorialNextBtn.textContent = 'İleri';
        }
    }

    // Önceki adıma git
    function goToPrevStep() {
        if (currentStep > 1) {
            currentStep--;
            updateTutorialView();
        }
    }

    // Sonraki adıma git
    function goToNextStep() {
        if (currentStep < totalSteps) {
            currentStep++;
            updateTutorialView();
        } else {
            // Son adımdaysak rehberi kapat
            closeTutorial();
        }
    }

    // Gösterge noktalarına tıklama işlevi
    function goToStep(step) {
        currentStep = step;
        updateTutorialView();
    }

    // Olay dinleyicileri
    if (tutorialCloseBtn) {
        tutorialCloseBtn.addEventListener('click', closeTutorial);
    }

    if (tutorialPrevBtn) {
        tutorialPrevBtn.addEventListener('click', goToPrevStep);
    }

    if (tutorialNextBtn) {
        tutorialNextBtn.addEventListener('click', goToNextStep);
    }

    // Gösterge noktalarına tıklama işlevini ekle
    tutorialIndicators.forEach(indicator => {
        indicator.addEventListener('click', function () {
            const step = parseInt(this.getAttribute('data-step'));
            goToStep(step);
        });
    });

    // ESC tuşuna basılınca rehberi kapat
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && tutorialOverlay.classList.contains('active')) {
            closeTutorial();
        }
    });
});
# 👹 Devour Ultimate Menu V4.2 - by G4T0XX

Bem-vindo ao **Devour Ultimate**, um dos mods/menus mais completos para o jogo DEVOUR, agora totalmente atualizado para o mapa **Carnival**!

Este projeto foi modificado, traduzido e atualizado por **G4T0XX** (Base original do código por ALittlePatate & Jadis0x).

## ✨ Funcionalidades (Features)

O menu conta com diversas secções repletas de funções para dominares o jogo:

* **👁️ Visuais (ESP):** Vê jogadores, Azazel/Kai, demónios, itens de ritual, chaves e animais através das paredes. Totalmente personalizável com cores e esqueletos (Skeleton ESP).
* **🏃 Jogador:** Fly Hack (Voar), Speed Hack (Correr rápido), Lanterna Forte, Brilho Máximo (Fullbright) e Luz UV Infinita.
* **🗺️ Específico do Mapa:** Vitória Instantânea (Host), queimar itens do ritual à distância, teletransporte para o Azazel/Kai, e opções para **remover inimigos** do mapa (Fantasmas, Aranhas, Corvos, Gosmas, Prisioneiros, etc).
* **📦 Itens & Spawn:** Puxa os itens do mapa para ti ou gera (spawn) novos itens, incluindo **Ingressos, Moedas e Cabeças de Boneca do Carnival**.
* **🏆 Diversos (Misc):** Desbloqueador automático de **100% das Conquistas da Steam** (incluindo Carnival!), destrancar todas as portas, forjar nível (Level Spoof), modificar EXP e criar servidores personalizados para até 30 jogadores.
* **🎮 Controlo de Jogadores (Host):** Prende amigos em jaulas, dá sustos (jumpscares), ressuscita, elimina ou teletransporta jogadores até ti.

## ⚙️ Requisitos

* Jogo DEVOUR original na Steam.
* [MelonLoader](https://melonwiki.xyz/#/) (Versão 0.6.0 ou superior).

## 🚀 Como Instalar (Para Jogadores)

1. Instala o MelonLoader na pasta principal do teu DEVOUR.
2. Descarrega o ficheiro compilado `.dll` nos "Releases" deste repositório.
3. Coloca o ficheiro `.dll` dentro da pasta `Mods` (criada pelo MelonLoader na raiz do jogo).
4. Inicia o jogo e diverte-te!

## ⌨️ Teclas de Atalho (Binds)

* **`INSERT`** : Abre e fecha o menu principal (Ocultar/Mostrar rato).
* **`F1` ao `F7`** : Navega rapidamente pelos separadores do menu.
* **Teclas Personalizáveis** : O botão de Voar (Fly) pode ser alterado diretamente no menu.

## 🛠️ Como Compilar o Código Fonte (Para Desenvolvedores)

Se descarregaste o código fonte e pretendes compilar a tua própria versão:

1. Certifica-te de que tens o `.NET SDK 6.0` instalado no teu computador.
2. Ajusta o ficheiro `DevourClient.csproj` para apontar para a localização exata da pasta do teu jogo (Ex: `C:\Program Files (x86)\Steam\steamapps\common\Devour\`).
3. Abre o terminal na pasta do projeto e introduz o comando:
   ```bash
   dotnet build

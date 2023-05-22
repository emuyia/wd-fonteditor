using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace wdfe
{
    // The FontEditor class, inherits from the Form class
    public partial class FontEditor : Form
    {
        // Stores font parameters like bytes, char width etc.
        private readonly FontParameters fontParams;

        // The FontEditor constructor
        public FontEditor()
        {
            // Initialize form components
            InitializeComponent();

            // Initialize FontParameters
            fontParams = new FontParameters();
        }

        // Constants for user interaction
        private const string VersionPrefix = "WDFE ";
        private const string HiddenText = "(๑•́ ₃ •̀๑)";

        private const string OpenFontTitle = "Open Font";
        private const string OpenFontFilter = "FNT|*.fnt|WFT|*.wft";
        private const string BitmapFilter = "Bitmap|*.bmp";

        private const string ExportedSymbolText1 = "Exported symbol #";
        private const string ExportedSymbolText2 = " to ";
        private const string OpenSymbolText = "Open Symbol";
        private const string SavedSymbolText = "Saved symbol #";

        private const string FontNotSupported = @"Font not supported.""";
        private const string FontNotSupported2 = @"""aa견고딕09.fnt"" is not currently supported.";
        private const string OpenedFontText = "Opened font ";
        private const string ImportedText1 = "Imported ";
        private const string ImportedText2 = " as symbol #";
        private const string ExportedFontText = "Exported font to ";

        private const string TutorialTitle = "Info";
        private const string TutorialText = "Shortcuts:" +
                                          "\nW = next symbol" +
                                          "\nS = previous symbol" +
                                          "\nA = decrease symbol width" +
                                          "\nD = increase symbol width" +
                                          "\n" +
                                          "\nExtra features:" +
                                          "\n- Click log text to open last used directory";

        // Event handler for FontEditor load event
        private void FontEditor_Load(object sender, EventArgs e)
        {
            // Get version from assembly information and display in Label_Version
            string version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            Label_Version.Text = VersionPrefix + version;
        }

        // --- Open Font ---
        // Event handler for OpenFont button click event
        private void Button_OpenFont_Click(object sender, EventArgs e)
        {
            // Creates an open file dialog to open a font file
            OpenFileDialog openFontDialog = new()
            {
                Title = OpenFontTitle,
                Filter = OpenFontFilter,
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            // Open the font file if a file is selected
            if (openFontDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFontFile(openFontDialog);
            }
        }

        // Opens a font file. It reads bytes from the file into the fontParams.FontBytes
        // Then it initializes font parameters and sets the default selected character.
        private void OpenFontFile(OpenFileDialog openFontDialog)
        {
            var fontByteList = new List<byte>();
            fontByteList.AddRange(File.ReadAllBytes(openFontDialog.FileName));

            fontParams.FontBytesUntouched.Clear();
            fontParams.FontBytesUntouched.AddRange(fontByteList);

            fontParams.CharWidthTable.Clear();

            HandleKnownWhiteDayFontFileSizes(fontByteList);

            // Remove 1 byte at the end of WFT file
            fontByteList.RemoveAt(fontByteList.Count - 1);

            // Remove WFT file header (14 bytes usually)
            fontByteList.RemoveRange(0, fontParams.HeaderTrim);

            // Put List back into byte[] array
            fontParams.FontBytes = fontByteList.ToArray();

            // Iterate backwards because editing list affects i
            for (int i = fontByteList.Count - 1; i-- > 0;) 
            {
                if (i % (fontParams.CharByteSpan + fontParams.TrimOffset) == 0)
                {
                    // Add charwidth value from byte to the start of the charWidthTable
                    fontParams.CharWidthTable.Insert(0, fontParams.FontBytes[i]);

                    // Remove two bytes every (usually) 650, to straighten out columns
                    fontParams.TrimOffset.RepeatAction(() => fontByteList.RemoveAt(i + 1));

                    // Get rid of charwidth value from the fontbytelist
                    fontByteList.RemoveAt(i);

                    // Replace with empty byte
                    fontByteList.Insert(i, 0x00); 
                }
            }

            // Mopping up bug I don't understand where charWidthTable isn't populated correctly
            fontParams.CharWidthTable.RemoveAt(0); 
            fontParams.CharWidthTable.Add(fontParams.FontBytes[^1]);

            // Put List back into byte[] array
            fontParams.FontBytes = fontByteList.ToArray(); 

            CharSelect.Maximum = fontParams.FontBytes.Length / fontParams.CharByteSpan;
            CharWidthSetting.Maximum = fontParams.PixelWidth - 1;

            CharSelect_ValueChanged(null, null);

            fontParams.LastLoadedFont = Path.GetFileName(openFontDialog.FileName);
            Log.Text = OpenedFontText + Path.GetFileName(openFontDialog.FileName);

            fontParams.LastDirUsed = Path.GetDirectoryName(openFontDialog.FileName);

            CharSelect.Enabled = true;
            CharSelect.Value = CharSelect.Minimum;
            CharWidthSetting.Enabled = true;
            Button_ExportBMP.Enabled = true;
            Button_ImportBMP.Enabled = true;
            Button_SaveFont.Enabled = true;
            Button_SaveSymbol.Enabled = true;
        }

        // Handles known font file sizes. It sets the CharByteSpan and CharCount values
        // based on the font file size.
        private void HandleKnownWhiteDayFontFileSizes(List<byte> fontByteList)
        {
            switch (fontByteList.Count)
            {
                case 171472:
                    Utils.ShowError(FontNotSupported2);
                    return;
                case 598288:
                    SetFontParams(18, 14, 2, 162);
                    break;
                case 2371216:
                    SetFontParams(36, 14, 2, 648);
                    break;
                default:
                    Utils.ShowError(FontNotSupported);
                    return;
            }
        }

        // Sets font parameters based on known font files
        private void SetFontParams(int pixelWidth, int headerTrim, int trimOffset, int charByteSpan)
        {
            fontParams.PixelWidth = pixelWidth;
            fontParams.HeaderTrim = headerTrim;
            fontParams.TrimOffset = trimOffset;
            fontParams.CharByteSpan = charByteSpan;
        }

        // --- Export BMP ---
        // Opens a dialog for selecting the file name and location to save the bitmap file.
        // It then saves the current character's bitmap representation to the selected file.
        private void Button_ExportBMP_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ShowSaveFileDialog(BitmapFilter, Convert.ToString(CharSelect.Value));

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    BitmapExtensions.SaveBitmap(fontParams.CharBMP, filePath);
                    Log.Text = $"{ExportedSymbolText1}{Convert.ToInt32(CharSelect.Value)}{ExportedSymbolText2}{Path.GetFileName(filePath)}";
                    fontParams.LastDirUsed = Path.GetDirectoryName(filePath);
                }
                catch (Exception bmpExportException)
                {
                    Utils.ShowError(bmpExportException.Message);
                }
            }
        }

        // --- Import BMP ---
        // Opens a dialog for selecting a bitmap file to import.
        // It then imports the selected bitmap file as the new character's bitmap representation.
        private void Button_ImportBMP_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ShowOpenFileDialog(BitmapFilter, OpenSymbolText);

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    ImportBitmap(filePath);

                    ViewCharBox.Image = BitmapExtensions.PrepareBMP4CharBox(fontParams.CharBMP,
                                                                            fontParams.PadPixelCount,
                                                                            fontParams.PixelWidth);
                    CharWidthSetting.Value = GetCharWidthSetting();

                    Log.Text = $"{ImportedText1}{Path.GetFileName(filePath)}{ImportedText2}{Convert.ToInt32(CharSelect.Value)}";

                    fontParams.LastDirUsed = Path.GetDirectoryName(filePath);
                }
                catch (Exception bmpImportException)
                {
                    // Show both the exception message and the stack trace for more detailed error reporting.
                    Utils.ShowError($"{bmpImportException.Message}\n\nStack Trace:\n{bmpImportException.StackTrace}");
                }
            }
        }

        // Handles the importing of a bitmap file.
        // Creates new bitmap object and byte array of the bitmap data.
        private void ImportBitmap(string filePath)
        {
            fontParams.CharBMP = new Bitmap(filePath);
            fontParams.CharBytes = BitmapExtensions.BmpToByteArray(filePath);
        }

        // --- Save Changes to Symbol ---
        // Calls the SaveSymbol method and triggers a change in CharSelect.
        private void Button_SaveSymbol_Click(object sender, EventArgs e)
        {
            SaveSymbol();

            CharSelect_ValueChanged(sender, e);
        }

        // Handles the process of saving symbol changes.
        // It updates the FontBytes array and CharWidthTable with the new values.
        private void SaveSymbol()
        {
            var fontByteList = new List<byte>(fontParams.FontBytes);
            int symbolPosition = fontParams.CharByteSpan * (int)(CharSelect.Value - 1);

            fontByteList.RemoveRange(symbolPosition, fontParams.CharByteSpan);
            fontByteList.InsertRange(symbolPosition, CharBytesPaddingRemoved());

            fontParams.FontBytes = fontByteList.ToArray();

            fontParams.CharWidthTable[(int)(CharSelect.Value - 1)] = (byte)CharWidthSetting.Value;

            Log.Text = $"{SavedSymbolText}{CharSelect.Value}";
        }

        // Returns a version of CharBytes with "padding" bytes removed from the character's byte list.
        // It iterates through the list in reverse and removes padding bytes at each step.
        private List<byte> CharBytesPaddingRemoved()
        {
            var charByteList = new List<byte>(fontParams.CharBytes);

            for (int i = charByteList.Count; i-- > 0;)
            {
                if (i % fontParams.BMPStride == 0)
                {
                    fontParams.PadByteCount.RepeatAction(() => charByteList.RemoveAt(i));
                }
            }

            return charByteList;
        }

        // --- Save Font ---
        // Allows the user to save the font changes they have made.
        private void Button_SaveFont_Click(object sender, EventArgs e)
        {
            string filePath = Utils.ShowSaveFileDialog(OpenFontFilter, fontParams.LastLoadedFont);

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    byte[] restoredFontBytes = PrepareFontBytesForSave();
                    File.WriteAllBytes(filePath, restoredFontBytes);
                    Log.Text = $"{ExportedFontText}{Path.GetFileName(filePath)}";
                    fontParams.LastDirUsed = Path.GetDirectoryName(filePath);
                }
                catch (Exception saveFontException)
                {
                    Utils.ShowError(saveFontException.Message);
                }
            }
        }

        // Handles the process of preparing the font bytes for saving
        // Does this by restoring the font bytes to their original format before saving.
        private byte[] PrepareFontBytesForSave()
        {
            var fontByteList = new List<byte>(fontParams.FontBytes);
            var charWidthTable = new byte[fontParams.CharWidthTable.Count + 1];
            fontParams.CharWidthTable.CopyTo(charWidthTable, 1);

            int charIndex = charWidthTable.Length - 2;

            for (int i = fontByteList.Count - 1; i-- > 0;)
            {
                if (i % (fontParams.CharByteSpan) == 0)
                {
                    fontParams.TrimOffset.RepeatAction(() => fontByteList.Insert(i + 1, 0x00));
                    fontByteList[i] = charWidthTable[charIndex--];
                }
            }

            fontByteList.Add(0x00);
            fontByteList.InsertRange(0, fontParams.FontBytesUntouched.Take(fontParams.HeaderTrim));

            return fontByteList.ToArray();
        }

        // --- Update Symbol Display ---
        // Updates the current character's bitmap representation and character width value when
        // the character select value is changed.
        private void CharSelect_ValueChanged(object sender, EventArgs e)
        {
            UpdateFontByteList();

            UpdateImagePreview();
        }

        // Converts the current character's bitmap representation to a byte array
        // and updates the corresponding bytes in the list of bytes that make up the font.
        private void UpdateFontByteList()
        {
            var fontByteList = new List<byte>();
            fontByteList.AddRange(fontParams.FontBytes);

            fontParams.BMPStride = Utils.RoundUp(fontParams.PixelWidth / 2, 4);

            int imagePixelWidth = fontParams.BMPStride * 2;
            fontParams.PadPixelCount = imagePixelWidth - fontParams.PixelWidth;

            fontParams.PadByteCount = fontParams.PadPixelCount / 2;

            fontParams.CharBytes = GetCharByteList(fontByteList)
                                               .ToArray();

            fontParams.CharBMP = BitmapExtensions.ByteTo4bppImage(fontParams.CharBytes,
                                                                  imagePixelWidth,
                                                                  fontParams.PixelWidth,
                                                                  fontParams.BMPStride);
        }

        // Retrieves the bytes representing the currently selected character from the font byte list.
        // It also includes padding bytes to make up the expected byte span of the character.
        private List<byte> GetCharByteList(List<byte> fontByteList)
        {
            var charByteList = fontByteList.GetRange(Convert.ToInt32(CharSelect.Value - 1) * fontParams.CharByteSpan, fontParams.CharByteSpan);

            for (int i = charByteList.Count; i-- > 0;)
                if (i % (fontParams.PixelWidth / 2) == 0)
                    fontParams.PadByteCount.RepeatAction(() => charByteList.Insert(i, 0xFF));

            return charByteList;
        }

        // Updates the image preview of the currently selected character.
        // It first clears the initial image of the character view box, then sets its image to the bitmap of the current character.
        // The character's width setting is also updated to reflect its actual value.
        private void UpdateImagePreview()
        {
            ViewCharBox.InitialImage = null;

            ViewCharBox.Image = BitmapExtensions.PrepareBMP4CharBox(fontParams.CharBMP,
                                                                    fontParams.PadPixelCount,
                                                                    fontParams.PixelWidth);

            CharWidthSetting.Value = GetCharWidthSetting();
        }

        // Retrieves the width setting of the currently selected character.
        // It first converts the character's width table to an array,
        // then accesses the width setting of the current character in the array.
        public int GetCharWidthSetting()
        {
            return Convert.ToInt32(fontParams.CharWidthTable.ToArray()[Convert.ToInt32(CharSelect.Value) - 1]);
        }

        // Updates the displayed width setting of the current character when it is changed
        private void CharWidthSetting_ValueChanged(object sender, EventArgs e)
        {
            Label_CharWidthSetting.Text = CharWidthSetting.Value.ToString();
        }

        // Enables the user to change the selected character and its width setting using the keyboard.
        // 'W' and 'S' keys for changing the character, 'D' and 'A' keys for changing the width setting.
        private void FontEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (CharSelect.Enabled)
            {
                if (e.KeyCode == Keys.W) if (CharSelect.Value != CharSelect.Maximum) CharSelect.Value += 1;
                if (e.KeyCode == Keys.S) if (CharSelect.Value != CharSelect.Minimum) CharSelect.Value -= 1;
            }
            if (CharWidthSetting.Enabled)
            {
                if (e.KeyCode == Keys.D) if (CharWidthSetting.Value != CharWidthSetting.Maximum) CharWidthSetting.Value += 1;
                if (e.KeyCode == Keys.A) if (CharWidthSetting.Value != CharWidthSetting.Minimum) CharWidthSetting.Value -= 1;
            }
        }


        // -- Misc --
        // Opens the directory where the last font file was used when the user clicks on the log.
        private void Log_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", fontParams.LastDirUsed);
        }

        // Displays a message box with the tutorial text when the user clicks on the version label.
        private void LabelVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TutorialText, TutorialTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        // Displays a message box with hidden text when the user clicks on the credit label.
        private void LabelCredit_Click(object sender, EventArgs e)
        {
            MessageBox.Show(HiddenText);
        }
    }
}

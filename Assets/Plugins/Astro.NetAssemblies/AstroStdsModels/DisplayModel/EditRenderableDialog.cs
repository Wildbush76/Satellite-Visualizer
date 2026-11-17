using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AFSPC.AstroStds.Models;

namespace AFSPC.AstroStandards.WinForms.DialogForms
{
   /// <summary>
   /// Provides a dialog form to edit the properties of a <see cref="RenderableModel"/> object.
   /// </summary>
   public partial class EditRenderableDialog : Form
   {
      #region - Fields -

      private List<RenderableModel> models;
      private bool applyAll;

      #endregion

      #region - Constructors -

      /// <summary>
      /// Initializes a new instance of the <see cref="EditRenderableDialog"/> class.
      /// </summary>
      /// <param name="model">The model.</param>
      public EditRenderableDialog(RenderableModel model)
      {
         if (model == null)
            throw new ArgumentNullException("model");

         List<RenderableModel> models = new List<RenderableModel>();
         models.Add(model);

         Initialize(models);
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="EditRenderableDialog"/> class.
      /// </summary>
      /// <param name="models">The list of models to edit.</param>
      public EditRenderableDialog(List<RenderableModel> models)
      {
         Initialize(models);
      }

      #endregion

      #region - Properties -

      /// <summary>
      /// Gets a value indicating whether [apply to all].
      /// </summary>
      /// <value><c>true</c> if [apply to all]; otherwise, <c>false</c>.</value>
      public bool ApplyToAll
      {
         get { return this.applyAll; }
      }

      #endregion

      #region - Methods -

      #region - Private -

      /// <summary>
      /// Initializes the dialog form.
      /// </summary>
      /// <param name="models">The models for which to edit the properties..</param>
      private void Initialize(List<RenderableModel> models)
      {
         if (models == null)
            throw new ArgumentNullException("models");
         if (models.Count == 0)
            throw new ArgumentException("models needs to have at least 1 element");

         InitializeComponent();

         this.models = models;
         this.txtLabel.Text = this.models[0].Label;
         this.pnlColor.BackColor = this.models[0].Color;
         this.txtColor.Text = this.models[0].Color.Name;

         this.Text = "Edit render options (single)";
         this.chkApply.Visible = true;
         if (this.models.Count > 1)
         {
            this.Text = "Edit render options (multiple)";
            this.chkApply.Visible = false;
            this.Size = new Size(247, 130);
         }
      }

      /// <summary>
      /// Shows a <see cref="ColorDialog"/> and stores the selected color in the specified panel and textbox.
      /// </summary>
      /// <param name="pnlColor"><see cref="Panel"/> for which the BackColor property holds the selected color.</param>
      /// <param name="txtColor"><see cref="TextBox"/> that shows the name of the selected color.</param>
      private void SelectColor(Panel pnlColor, TextBox txtColor)
      {
         ColorDialog dialog = new ColorDialog();
         dialog.Color = pnlColor.BackColor;
         dialog.FullOpen = true;

         if (dialog.ShowDialog() == DialogResult.OK)
         {
            pnlColor.BackColor = dialog.Color;
            txtColor.Text = dialog.Color.Name;
         }
      }

      #endregion

      #region - Eventhandlers -

      /// <summary>
      /// Handles the Click event of the btnOK control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
      private void btnOK_Click(object sender, EventArgs e)
      {
         foreach (RenderableModel model in models)
         {
            model.Label = txtLabel.Text.Trim();
            model.Color = pnlColor.BackColor;
         }

         this.applyAll = this.chkApply.Checked;
         if (this.applyAll)
         {
            models[0].AssignBaseColor(models[0].Label, models[0].Color);
         }

         this.Close();
      }

      /// <summary>
      /// Handles the Click event of the btnCancel control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
      private void btnCancel_Click(object sender, EventArgs e)
      {
         this.applyAll = false;
         this.Close();
      }

      /// <summary>
      /// Handles the Click event of the btnColor control.
      /// </summary>
      /// <param name="sender">The source of the event.</param>
      /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
      private void btnColor_Click(object sender, EventArgs e)
      {
         SelectColor(this.pnlColor, this.txtColor);
      }

      #endregion

      #endregion
   }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DataGridViewApplication
{
    //将表格逻辑从主窗体分离开来
    class DataGridViewShow
    {
        DataGridView showContent;

        //IContainerControl parentContainer;

        public DataGridViewShow(Control parentContainer)
        {
            Init();
            parentContainer.Controls.Add(showContent);
            TestFillContent();
        }

        private void Init()
        {
            showContent = new DataGridView();
            CreateShowContent();
            showContent.CellMouseUp += ShowContent_CellMouseUp;
            showContent.CellStateChanged += ShowContent_CellStateChanged;
        }

        private void CreateShowContent()
        {
            showContent.MultiSelect = false;
            showContent.ShowCellToolTips = false;
            //showContent.AutoResizeRows();
            showContent.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            showContent.AllowUserToAddRows = false;
            showContent.AllowUserToDeleteRows = false;
            showContent.AllowUserToOrderColumns = false;
            showContent.AllowUserToResizeColumns = false;
            showContent.AllowUserToResizeRows = false;

            //定义默认单元格样式
            DataGridViewCellStyle defaultCellStyle = new DataGridViewCellStyle();
            defaultCellStyle.Padding = new Padding(5, 3, 5, 3);
            defaultCellStyle.WrapMode = DataGridViewTriState.True;//换行只对中文字符自动换行，如果是英文/数字且没有换行符不会换行，需要自己根据宽度赋予换行符

            showContent.DefaultCellStyle = defaultCellStyle;

            //DataGridViewColumn indexCol = new DataGridViewColumn();
            DataGridViewTextBoxColumn indexCol = new DataGridViewTextBoxColumn();
            indexCol.Width = 50;
            indexCol.ReadOnly = true;
            indexCol.Resizable = DataGridViewTriState.False;
            showContent.Columns.Add(indexCol);

            DataGridViewTextBoxColumn contentCol = new DataGridViewTextBoxColumn();
            contentCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            contentCol.Resizable = DataGridViewTriState.False;
            contentCol.ReadOnly = true;
            showContent.Columns.Add(contentCol);

            int imageColWidth = 35; //imageWidth:24 + leftpadding:5 + rightpadding:5

            DataGridViewImageColumn deleteCol = new DataGridViewImageColumn();
            deleteCol.Width = imageColWidth;
            deleteCol.ReadOnly = true;
            deleteCol.Resizable = DataGridViewTriState.False;
            deleteCol.Image = Properties.Resources.DeleteIcon;
            deleteCol.ImageLayout = DataGridViewImageCellLayout.Normal;
            showContent.Columns.Add(deleteCol);

            DataGridViewImageColumn editCol = new DataGridViewImageColumn();
            editCol.Width = imageColWidth;
            editCol.ReadOnly = true;
            editCol.Resizable = DataGridViewTriState.False;
            editCol.Image = Properties.Resources.EditIcon;
            editCol.ImageLayout = DataGridViewImageCellLayout.Normal;
            showContent.Columns.Add(editCol);

            DataGridViewImageColumn forwardMoveCol = new DataGridViewImageColumn();
            forwardMoveCol.Width = imageColWidth;
            forwardMoveCol.ReadOnly = true;
            forwardMoveCol.Resizable = DataGridViewTriState.False;
            forwardMoveCol.Image = Properties.Resources.UpArrowEnableIcon;
            forwardMoveCol.ImageLayout = DataGridViewImageCellLayout.Normal;
            showContent.Columns.Add(forwardMoveCol);

            DataGridViewImageColumn backMoveCol = new DataGridViewImageColumn();
            backMoveCol.Width = imageColWidth;
            backMoveCol.ReadOnly = true;
            backMoveCol.Resizable = DataGridViewTriState.False;
            backMoveCol.Image = Properties.Resources.DownArrowEnableIcon;
            backMoveCol.ImageLayout = DataGridViewImageCellLayout.Normal;
            showContent.Columns.Add(backMoveCol);

            showContent.ColumnHeadersVisible = false;
            showContent.RowHeadersVisible = false;
            showContent.Dock = DockStyle.Fill;
        }

        //做一些业务逻辑上的处理，通过单元格的选择和取消来实现单元格禁用和单元格点击的效果
        //1.
        private void ShowContent_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.StateChanged == DataGridViewElementStates.Selected && !BCanOperateCell(e.Cell))
                e.Cell.Selected = false;
        }

        //通过mouseUp取消选择来充当点击效果
        private void ShowContent_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (BCanOperateCell(e.RowIndex, e.ColumnIndex))
            {
                //showContent.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                ProcessImageBtnFunction(e.RowIndex,e.ColumnIndex);
                showContent.CurrentCell.Selected = false;
            }
        }

        private bool BCanOperateCell(DataGridViewCell targetCell)
        {
            return BCanOperateCell(targetCell.RowIndex,targetCell.ColumnIndex);
        }

        private bool BCanOperateCell(int rowIndex,int columnIndex)
        {
            if (columnIndex == 0 || columnIndex == 1
                || (rowIndex == 0 && columnIndex == 4)
                || (rowIndex == showContent.Rows.Count - 1 && columnIndex == 5))
                return false;
            return true;
        }

        //对内容的一些操作

        private void ProcessImageBtnFunction(int rowIndex,int colIndex)
        {
            bool bNeedRelimit = false;
            switch (colIndex)
            {
                case 2:
                    DeleteRow(rowIndex);
                    bNeedRelimit = true;
                    break;
                case 3:
                    EditRowContent(rowIndex);
                    break;
                case 4:
                    ForwardMovement(rowIndex);
                    bNeedRelimit = true;
                    break;
                case 5:
                    BackMovement(rowIndex);
                    bNeedRelimit = true;
                    break;
            }
            if(bNeedRelimit == true)
                ReLimit();
        }

        //新增一行 目前没有实现
        private void AddNewRow(string content)
        {
            int rowIndex = showContent.Rows.Add();
            showContent[0, rowIndex].Value = CreateIndexCellValue(rowIndex);
            showContent[1, rowIndex].Value = content;
        }

        //删除一行
        private void DeleteRow(int rowIndex)
        {
            showContent.Rows.RemoveAt(rowIndex);
        }

        //编辑行内容
        private void EditRowContent(int rowIndex)
        {
            ContentEditForm editForm = new ContentEditForm();
            editForm.Content = showContent[1, rowIndex].Value.ToString();

            if (DialogResult.OK == editForm.ShowDialog())
            {
                showContent[1, rowIndex].Value = editForm.Content;
            }
        }

        //上移
        private void ForwardMovement(int rowIndex)
        {
            DataGridViewRow targetRow = showContent.Rows[rowIndex];
            showContent.Rows.RemoveAt(rowIndex);
            showContent.Rows.Insert(rowIndex - 1,targetRow);
        }

        //下移
        private void BackMovement(int rowIndex)
        {
            DataGridViewRow targetRow = showContent.Rows[rowIndex];
            showContent.Rows.RemoveAt(rowIndex);
            showContent.Rows.Insert(rowIndex + 1, targetRow);
        }

        //当我们对内容操作之后需要对内容重新进行限制

        private void ReLimit()
        {
            if (showContent.Rows.Count == 1)
            {
                showContent[0, 0].Value = CreateIndexCellValue(0);
                showContent[4, 0].Value = Properties.Resources.UpArrowDisableIcon;
                showContent[5, 0].Value = Properties.Resources.DownArrowDisableIcon;
            }
            else if (showContent.Rows.Count > 1)
            {
                for (int i = 0; i < showContent.Rows.Count; i++)
                {
                    showContent[0, i].Value = CreateIndexCellValue(i);
                    if (i == 0)
                    {
                        showContent[4, i].Value = Properties.Resources.UpArrowDisableIcon;
                        showContent[5, i].Value = Properties.Resources.DownArrowEnableIcon;
                    }
                    else if (i == showContent.Rows.Count - 1)
                    {
                        showContent[4, i].Value = Properties.Resources.UpArrowEnableIcon;
                        showContent[5, i].Value = Properties.Resources.DownArrowDisableIcon;
                    }
                    else
                    {
                        showContent[4, i].Value = Properties.Resources.UpArrowEnableIcon;
                        showContent[5, i].Value = Properties.Resources.DownArrowEnableIcon;
                    }
                }
            }
        }

        //获取Index单元格的内容
        private string CreateIndexCellValue(int index)
        {
            return string.Format("{0}.",index);
        }

        //填充测试数据
        private void TestFillContent()
        {
            for (int i = 0; i < 10; i++)
            {
                int index = showContent.Rows.Add();
                showContent.Rows[index].Cells[0].Value = index + ".";
                showContent.Rows[index].Cells[1].Value = "testContent" + i;
            }
            ReLimit();
        }
    }
}

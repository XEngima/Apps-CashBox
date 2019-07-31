using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBase.BusinessLayer;
using EasyBase.Classes;

namespace EasyBase.BusinessLayer
{
    public class TagComboBoxItem
    {
        public TagHandlerAction Action;
        public AccountTag AccountTag;
        public string DisplayText;

        public override string ToString()
        {
            return DisplayText;
        }
    }

    public enum TagHandlerAction
    {
        Untagged = 0,
        Specified = 1,
        Split = 2
    }

    public sealed class TagHandler
    {
        public TagHandler(DataCache dataCache, int accountNo)
        {
            DataCache = dataCache;
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();
                AccountTags = core.GetAccountTagsByAccountNo(accountNo);
            }

            AccountBalance = DataCache.CalculateAccountBalance(accountNo);

            CreateComboBoxItems();
        }

        private DataCache DataCache { get; set; }

        private AccountTagCollection AccountTags
        {
            get;
            set;
        }

        private decimal AccountBalance
        {
            get;
            set;
        }

        public TagComboBoxItem GetDefaultComboBoxItem()
        {
            int specifiedCount = 0;
            decimal totalRelativeValue = 0;

            TagComboBoxItem relativeItemWith100Percent = null;
            TagComboBoxItem firstSplitItem = null;
            TagComboBoxItem untaggedItem = null;

            foreach (var comboBoxItem in ComboBoxItems)
            {
                if (comboBoxItem.Action == TagHandlerAction.Specified)
                {
                    specifiedCount++;

                    if (comboBoxItem.AccountTag.Type == AccountTagType.PercentOfRest)
                    {
                        totalRelativeValue += comboBoxItem.AccountTag.RelativeValue;
                    }

                    if (comboBoxItem.AccountTag.RelativeValue == 1)
                    {
                        relativeItemWith100Percent = comboBoxItem;
                    }
                }
                else if (comboBoxItem.Action == TagHandlerAction.Split)
                {
                    firstSplitItem = comboBoxItem;
                }
                else if (comboBoxItem.Action == TagHandlerAction.Untagged)
                {
                    untaggedItem = comboBoxItem;
                }
                else
                {
                    throw new NotImplementedException("Unknown action " + comboBoxItem.Action + " on combo box item.");
                }
            }

            // If there is only one item in the list, return that item
            if (ComboBoxItems.Count() == 1)
            {
                return ComboBoxItems.First();
            }

            // If there is one specified and relative item with 100%, return that item
            if (relativeItemWith100Percent != null)
            {
                return relativeItemWith100Percent;
            }

            // If the unmarked amount has 100%, return that item
            if (totalRelativeValue == 0)
            {
                return untaggedItem;
            }

            // If there is one split item, return that item
            if (firstSplitItem != null)
            {
                return firstSplitItem;
            }

            // As a default, return first item
            return ComboBoxItems.First();
        }

        public List<TagComboBoxItem> ComboBoxItems { get; private set; }

        private void CreateComboBoxItems()
        {
            var tagOptions = new List<TagComboBoxItem>();

            int relativeCount = 0;
            string relativeText = "";
            string relativeSlash = "";
            decimal untaggedPercent = 100m;
            decimal total = 0m;

            foreach (var accountTag in AccountTags)
            {
                total += accountTag.Amount;

                string text;
                if (accountTag.Type == AccountTagType.ExactAmount)
                {
                    text = accountTag.TagAccountName + " (" +
                            accountTag.MoneyValue.ToString(CurrentApplication.MoneyDisplayFormat) + " kr)";
                }
                else
                {
                    text = accountTag.TagAccountName + " (" +
                            (accountTag.RelativeValue * 100m).ToString("0.#######") + "%)";

                    relativeCount++;
                    relativeText += relativeSlash + accountTag.TagAccountName;
                    relativeSlash = "/";

                    untaggedPercent -= accountTag.RelativeValue * 100;
                }

                tagOptions.Add(new TagComboBoxItem()
                {
                    Action = TagHandlerAction.Specified,
                    AccountTag = accountTag,
                    DisplayText = text
                });
            }

            if (untaggedPercent > 0)
            {
                relativeText += "/Omärkta";
            }

            if (relativeCount > 1)
            {
                tagOptions.Add(new TagComboBoxItem()
                {
                    Action = TagHandlerAction.Split,
                    AccountTag = null,
                    DisplayText = "Fördela över " + relativeText
                });
            }

            string displayText = "Omärkta (" + (AccountBalance - total).ToString(CurrentApplication.MoneyDisplayFormat) + " kr";

            if (untaggedPercent > 0 && untaggedPercent < 100)
            {
                displayText += " / " + untaggedPercent.ToString("0.#######") + "%";
            }

            displayText += ")";

            tagOptions.Add(new TagComboBoxItem()
            {
                Action = TagHandlerAction.Untagged,
                AccountTag = null,
                DisplayText = displayText
            });

            ComboBoxItems = tagOptions;
        }
    }
}
